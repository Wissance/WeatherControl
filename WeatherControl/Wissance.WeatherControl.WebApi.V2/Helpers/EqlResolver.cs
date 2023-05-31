using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wissance.WeatherControl.GraphData;


namespace Wissance.WeatherControl.WebApi.V2.Helpers
{
    public class EqlResolver
    {
        public string GetQueryToCountItems(ModelType model)
        {
            return String.Format(_selectCountQueries[model]);
        }
        public string GetQueryToFetchManyItems(ModelType model, int offset, int limit, IDictionary<string, string> parameters)
        {
            if (!_selectManyWithLimitsQueries.ContainsKey(model))
                return null;
            StringBuilder filterStr = new StringBuilder("");
            if (parameters != null && parameters.Any())
            {
                bool isFirst = true;
                filterStr.Append(" FILTER ");
                IDictionary<string, string> usingParams = _filterParamsTemplate.Where(fp => parameters.ContainsKey(fp.Key)).ToDictionary(p => p.Key, p=> p.Value);
                foreach (KeyValuePair<string,string> param in usingParams)
                {
                    // to pass + for timezone should be used %2B, i.e. - http://localhost:8058/api/Measurement?sensor=30ac8366-ea4b-11ed-ab17-e38db76a95f0&to=2023-05-01T12:01:00%2B05:00
                    string filterParamVal = string.Format(_filterParamsTemplate[param.Key], parameters[param.Key]);
                    if (!isFirst)
                        filterStr.Append(" AND ");
                    filterStr.Append(filterParamVal);
                    isFirst = false;
                }
            }

            string query = String.Format(_selectManyWithLimitsQueries[model], filterStr, offset, limit);

            return query;
        }
        
        public string GetQueryToGetOneItem(ModelType model)
        {
            if (!_selectOneByIdQuery.ContainsKey(model))
                return null;
            return String.Format(_selectOneByIdQuery[model]);
        }
        
        public string GetQueryToInsertItem(ModelType model)
        {
            if (!_insertQuery.ContainsKey(model))
                return null;
            return String.Format(_insertQuery[model]);
        }

        public string GetQueryToInsertMultipleItems(ModelType model, int number)
        {
            if (!_bulkInsertQuery.ContainsKey(model))
                return null;
            /*StringBuilder query = new StringBuilder("for x in {} union ( ");
            for (int i = 0; i < number; i++)
            {
                query.Append(String.Format(_bulkInsertQuery[model], i.ToString()));
            }

            query.Append(" );");
            return query.ToString();*/
            return _bulkInsertQuery[model];
        }
        
        public string GetQueryToGetManyItemsWithFilterById(ModelType model)
        {
            if (!_selectManyWithFilterById.ContainsKey(model))
                return null;
            return String.Format(_selectManyWithFilterById[model]);
        }

        public string GetQueryToUpdateItem(ModelType model)
        {
            if (!_updateQuery.ContainsKey(model))
                return null;
            return String.Format(_updateQuery[model]);
        }
        
        public string GetQueryToUpdateMultipleItems(ModelType model, int number)
        {
            if (!_bulkUpdateQuery.ContainsKey(model))
                return null;
            StringBuilder query = new StringBuilder("UNION ( ");
            for (int i = 0; i < number; i++)
            {
                query.Append(String.Format(_bulkUpdateQuery[model], i.ToString()));
            }

            query.Append(" )");
            return query.ToString();
        }
        
        public string GetQueryToDeleteItem(ModelType model)
        {
            if (!_deleteQuery.ContainsKey(model))
                return null;
            return String.Format(_deleteQuery[model]);
        }
        
        public string GetQueryToDeleteMultipleItems(ModelType model)
        {
            if (!_deleteQuery.ContainsKey(model))
                return null;
            return String.Format(_deleteQuery[model]);
        }

        private readonly IDictionary<ModelType, string> _selectCountQueries = new Dictionary<ModelType, string>()
        {
            {ModelType.MeasureUnit, "SELECT count ((SELECT MeasureUnit {{id}}))"},
            {ModelType.Measurement, "SELECT count ((SELECT Measurement {{id}}))"},
            {ModelType.Sensor , "SELECT count ((SELECT Sensor {{id}}))"},
            {ModelType.MeteoStation, "SELECT count ((SELECT MeteoStation {{id}}))" }
        };

        private readonly IDictionary<ModelType, string> _selectManyWithLimitsQueries = new Dictionary<ModelType, string>()
        {
            {ModelType.MeasureUnit, "SELECT MeasureUnit {{id, Name, Abbreviation, Description}} {0} OFFSET {1} LIMIT {2}"},
            {ModelType.Measurement, "SELECT Measurement {{id, SampleDate, Value, Unit:{{id, Name, Abbreviation, Description}}, Sensor:{{id, Name, Longitude, Latitude}} }} {0} OFFSET {1} LIMIT {2}"},
            {ModelType.Sensor , "SELECT Sensor {{id, Name, Latitude, Longitude, Measurements:{{id, SampleDate, Value, Unit:{{id, Name, Abbreviation}}, Sensor:{{id}} }} }} {0} OFFSET {1} LIMIT {2}"},
            {ModelType.MeteoStation, "SELECT MeteoStation {{id, Latitude, Longitude, Sensors:{{ id, Name, Latitude, Longitude, Measurements:{{id, SampleDate, Value, Unit:{{id, Name, Abbreviation}}, Sensor:{{id}} }} }} }} {0} OFFSET {1} LIMIT {2}" }
        };

        private readonly IDictionary<ModelType, string> _selectManyWithFilterById = new Dictionary<ModelType, string>()
        {
            {ModelType.Measurement, @"SELECT Measurement {{id, SampleDate, Value, Unit:{{id, Name, Abbreviation, Description}}, Sensor:{{id, Name, Longitude, Latitude}} }} FILTER .id IN array_unpack(<array<uuid>>$idList)"}
        };
        
        private readonly IDictionary<ModelType, string> _selectOneByIdQuery = new Dictionary<ModelType, string>()
        {
            // example: select MeasureUnit filter .id = <uuid>"91bedeac-9405-11ed-b635-2f706f53263b"
            {ModelType.MeasureUnit, @"SELECT MeasureUnit {{id, Name, Abbreviation, Description}} FILTER .id = <uuid>$id"},
            {ModelType.Measurement, @"SELECT Measurement {{id, SampleDate, Value, Unit:{{id, Name, Abbreviation, Description}}, Sensor:{{id, Name, Longitude, Latitude}} }} FILTER .id = <uuid>$id"},
            {ModelType.Sensor , "SELECT Sensor {{id, Name, Latitude, Longitude, Measurements:{{id, SampleDate, Value, Unit:{{id, Name, Abbreviation}}, Sensor:{{id}} }} }} FILTER .id = <uuid>$id"},
            {ModelType.MeteoStation, "SELECT MeteoStation {{id, Latitude, Longitude, Sensors:{{ id, Name, Latitude, Longitude, Measurements:{{id, SampleDate, Value, Unit:{{id, Name, Abbreviation}}, Sensor:{{id}} }} }} }} FILTER .id = <uuid>$id" }
        };

        private readonly IDictionary<ModelType, string> _insertQuery = new Dictionary<ModelType, string>()
        {
            //TODO(UMV): convert to decimal implemented with a HACK!!!
            {ModelType.Measurement, @"INSERT Measurement {{id:=<uuid>$id, SampleDate:=<datetime>$SampleDate, Value:=to_decimal(<str>$Value), Unit:=(SELECT MeasureUnit {{id, Name, Abbreviation, Description}} FILTER .id = <uuid>$MeasureUnitId LIMIT 1), "+ 
                                     "Sensor:=(SELECT Sensor {{id, Name, Latitude, Longitude }} FILTER .id = <uuid>$SensorId  LIMIT 1) }}"},
            {ModelType.Sensor, "INSERT Sensor {{id:=<uuid>$id, Name:=<str>$Name, Latitude:=<str>$Latitude, Longitude:=<str>$Longitude}}"},
            {ModelType.MeteoStation, "INSERT MeteoStation {{id:=<uuid>$id, Latitude:=<str>$Latitude, Longitude:=<str>$Longitude}}"}
        };
        
        // IMPORTANT !!!!!! here MUST be start and trailing SPACES
        private readonly IDictionary<ModelType, string> _bulkInsertQuery = new Dictionary<ModelType, string>()
        {
            //{ModelType.Measurement, @" INSERT Measurement {{id:=<uuid>$id{0}, SampleDate:=<datetime>$SampleDate{0}, Value:=to_decimal(<str>$Value{0}), Unit:=(SELECT MeasureUnit {{id, Name, Abbreviation, Description}} FILTER .id = <uuid>$MeasureUnitId{0} LIMIT 1), " +
            //                                    "Sensor:=(SELECT Sensor {{id, Name, Latitude, Longitude }} FILTER .id = <uuid>$SensorId{0}  LIMIT 1) }} "}
            { ModelType.Measurement, @"FOR x IN {{array_unpack(<array<json>>$data) }} UNION (INSERT Measurement {{id:=<uuid>x['id'], SampleDate:=<datetime>x['SampleDate'], Value:=to_decimal(<str>x['Value']), Unit:=(SELECT MeasureUnit {{id}} FILTER .id = <uuid>x['MeasureUnitId']), Sensor:=(SELECT Sensor {{id}} FILTER .id = <uuid>x['SensorId']  LIMIT 1) }});"}
        };

        private readonly IDictionary<ModelType, string> _updateQuery = new Dictionary<ModelType, string>()
        {
            {ModelType.Measurement, @"UPDATE Measurement FILTER .id = <uuid>$id SET {{ SampleDate:=<datetime>$SampleDate, Value:=to_decimal(<str>$Value) }}"},
            {ModelType.Sensor, "UPDATE Sensor FILTER .id = <uuid>$id SET {{ Name:=<str>$Name, Latitude:=<str>$Latitude, Longitude:=<str>$Longitude, Measurements:=(SELECT Measurement FILTER .id IN array_unpack(<array<uuid>>$Measurements) ) }}"},
            {ModelType.MeteoStation, "UPDATE MeteoStation FILTER .id = <uuid>$id SET {{ Latitude:=<str>$Latitude, Longitude:=<str>$Longitude, Sensors:=(SELECT Sensor FILTER .id IN array_unpack(<array<uuid>>$Sensors) ) }}"},
        };
        
        // IMPORTANT !!!!!! here MUST be start and trailing SPACES
        private readonly IDictionary<ModelType, string> _bulkUpdateQuery = new Dictionary<ModelType, string>()
        {
            { ModelType.Measurement , @"UPDATE Measurement FILTER .id = <uuid>$id{0} SET {{ SampleDate:=<datetime>$SampleDate{0}, Value:=to_decimal(<str>$Value{0}) }}"}
        };

        private readonly IDictionary<ModelType, string> _deleteQuery = new Dictionary<ModelType, string>()
        {
            {ModelType.Measurement, @"DELETE Measurement FILTER .id = <uuid>$id"},
            {ModelType.Sensor, @"DELETE Sensor FILTER .id = <uuid>$id"},
            {ModelType.MeteoStation, @"DELETE MeteoStation FILTER .id = <uuid>$id"}
        };
        
        private readonly IDictionary<ModelType, string> _deleteManyWithFilterById = new Dictionary<ModelType, string>()
        {
            {ModelType.Measurement, @"DELETE Measurement FILTER .id IN array_unpack(<array<uuid>>$idList)"}
        };

        private IDictionary<string, string> _filterParamsTemplate = new Dictionary<string, string>()
        {
            {SensorParam, " .Sensor.id=<uuid>\"{0}\" "},
            {MeasureFromParam, " .SampleDate>=to_datetime(\'{0}\') "},
            {MeasureToParam, " .SampleDate<=to_datetime(\'{0}\') "}
        };

        //private const string BulkOperationWrapper = "UNION({0})";

        private const string StationParam = "station";
        private const string SensorParam = "sensor";
        private const string MeasureFromParam = "from";
        private const string MeasureToParam = "to";
    }
}
