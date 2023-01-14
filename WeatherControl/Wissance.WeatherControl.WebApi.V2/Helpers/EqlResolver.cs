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

        private readonly IDictionary<ModelType, string> _selectManyWithLimitsQueries = new Dictionary<ModelType, string>()
        {
            {ModelType.MeasureUnit, "SELECT MeasureUnit OFFSET {0} LIMIT {1}"}
        };
        
        private readonly IDictionary<ModelType, string> _selectOneByIdQuery = new Dictionary<ModelType, string>()
        {
            {ModelType.MeasureUnit, "SELECT MeasureUnit"}
        };
    }
}
