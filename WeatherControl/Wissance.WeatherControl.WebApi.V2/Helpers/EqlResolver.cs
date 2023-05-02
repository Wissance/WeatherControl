using System;
using System.Collections.Generic;
using System.Linq;
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
            if (parameters != null && parameters.Any())
            {
                //IDictionary<string, string> = _filterParamsTemplate.Where(fp => parameters.ContainsKey(fp.Key)).ToDictionary(p => p.Key, p=> p.Value);
            }

            return String.Format(_selectManyWithLimitsQueries[model], offset, limit);
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
        
        public string GetQueryToUpdateItem(ModelType model)
        {
            if (!_updateQuery.ContainsKey(model))
                return null;
            return String.Format(_updateQuery[model]);
        }
        
        public string GetQueryToDeleteItem(ModelType model)
        {
            if (!_deleteQuery.ContainsKey(model))
                return null;
            return String.Format(_deleteQuery[model]);
        }

        private readonly IDictionary<ModelType, string> _selectCountQueries = new Dictionary<ModelType, string>()
        {
            {ModelType.MeasureUnit, "SELECT count ((SELECT MeasureUnit {{id}}))"},
            {ModelType.Measurement, "SELECT count ((SELECT Measurement {{id}})"},
            {ModelType.Sensor , "SELECT count ((SELECT Sensor {{id}}))"},
            {ModelType.MeteoStation, "SELECT count ((SELECT MeteoStation {{id}}))" }
        };

        private readonly IDictionary<ModelType, string> _selectManyWithLimitsQueries = new Dictionary<ModelType, string>()
        {
            {ModelType.MeasureUnit, "SELECT MeasureUnit {{id, Name, Abbreviation, Description}} OFFSET {0} LIMIT {1}"},
            {ModelType.Measurement, "SELECT Measurement {{id, SampleDate, Value, Unit:{{id, Name, Abbreviation, Description}}, Sensor:{{id, Name, Longitude, Latitude}} }} OFFSET {0} LIMIT {1}"},
            {ModelType.Sensor , "SELECT Sensor {{id, Name, Latitude, Longitude, Measurements:{{id, SampleDate, Value, Unit:{{id, Name, Abbreviation}}, Sensor:{{id}} }} }} OFFSET {0} LIMIT {1}"},
            {ModelType.MeteoStation, "SELECT MeteoStation {{id, Latitude, Longitude, Sensors:{{ id, Name, Latitude, Longitude, Measurements:{{id, SampleDate, Value, Unit:{{id, Name, Abbreviation}}, Sensor:{{id}} }} }} }} OFFSET {0} LIMIT {1}" }
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

        private readonly IDictionary<ModelType, string> _updateQuery = new Dictionary<ModelType, string>()
        {
            {ModelType.Measurement, @"UPDATE Measurement FILTER .id = <uuid>$id SET {{ SampleDate:=<datetime>$SampleDate, Value:=to_decimal(<str>$Value) }}"},
            {ModelType.Sensor, "UPDATE Sensor FILTER .id = <uuid>$id SET {{ Name:=<str>$Name, Latitude:=<str>$Latitude, Longitude:=<str>$Longitude, Measurements:=(SELECT Measurement FILTER .id IN array_unpack(<array<uuid>>$Measurements) ) }}"},
            {ModelType.MeteoStation, "UPDATE MeteoStation FILTER .id = <uuid>$id SET {{ Latitude:=<str>$Latitude, Longitude:=<str>$Longitude, Sensors:=(SELECT Sensor FILTER .id IN array_unpack(<array<uuid>>$Sensors) ) }}"},
        };

        private readonly IDictionary<ModelType, string> _deleteQuery = new Dictionary<ModelType, string>()
        {
            {ModelType.Measurement, @"DELETE Measurement FILTER .id = <uuid>$id"},
            {ModelType.Sensor, @"DELETE Sensor FILTER .id = <uuid>$id"},
            {ModelType.MeteoStation, @"DELETE MeteoStation FILTER .id = <uuid>$id"}
        };

        private IDictionary<string, string> _filterParamsTemplate = new Dictionary<string, string>()
        {
            {SensorParam, $".Sensor.Id=<uuid>${SensorParam}"},
            {MeasureFromParam, $".SampleDate>=<datetime>{MeasureFromParam}"},
            {MeasureToParam, $".SampleDate<=<datetime>{MeasureToParam}"}
        };

        private const string StationParam = "station";
        private const string SensorParam = "sensor";
        private const string MeasureFromParam = "from";
        private const string MeasureToParam = "to";
    }
}
