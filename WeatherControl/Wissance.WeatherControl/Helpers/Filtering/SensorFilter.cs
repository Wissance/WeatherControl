using System.Collections.Generic;
using Wissance.WeatherControl.Data.Entity;
using Wissance.WeatherControl.WebApi.Filters;

namespace Wissance.WeatherControl.WebApi.Helpers.Filtering
{
    public class SensorFilter
    {
        public static bool Filter(SensorEntity entity, IDictionary<string, string> parameters)
        {
            if (parameters.ContainsKey(FilterParamsNames.MeasureUnitParam))
            {
                return entity.MeasureUnitId.ToString() == parameters[FilterParamsNames.MeasureUnitParam];
            }
            
            if (parameters.ContainsKey(FilterParamsNames.StationParameter))
            {
                return entity.StationId.ToString() == parameters[FilterParamsNames.StationParameter];
            }

            return true;
        }
    }
}