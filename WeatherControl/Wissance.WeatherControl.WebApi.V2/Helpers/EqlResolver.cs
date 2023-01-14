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
            {ModelType.MeasureUnit, "SELECT MeasureUnit OFFSET {0} LIMIT {1}"}
        };
        
        private readonly IDictionary<ModelType, string> _selectOneByIdQuery = new Dictionary<ModelType, string>()
        {
            // example: select MeasureUnit filter .id = <uuid>"91bedeac-9405-11ed-b635-2f706f53263b"
            {ModelType.MeasureUnit, @"SELECT MeasureUnit filter .id = <uuid>$id"}
        };
    }
}
