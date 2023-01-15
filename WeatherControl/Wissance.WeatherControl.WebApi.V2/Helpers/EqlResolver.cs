using System;
using System.Collections.Generic;
using Wissance.WeatherControl.GraphData;


namespace Wissance.WeatherControl.WebApi.V2.Helpers
{
    public class EqlResolver
    {
        public string GetQueryToFetchManyItems(ModelType model, int offset, int limit)
        {
            if (!_selectManyWithLimitsQueries.ContainsKey(model))
                return null;
            return String.Format(_selectManyWithLimitsQueries[model], offset, limit);
        }
        
        public string GetQueryToGetOneItem(ModelType model)
        {
            if (!_selectOneByIdQuery.ContainsKey(model))
                return null;
            return String.Format(_selectOneByIdQuery[model]);
        }

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
    }
}
