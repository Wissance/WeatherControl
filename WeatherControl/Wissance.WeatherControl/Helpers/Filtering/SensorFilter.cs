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
                if (entity.MeasureUnitId.ToString() != parameters[FilterParamsNames.MeasureUnitParam])
                    return false;
            }
            
            if (parameters.ContainsKey(FilterParamsNames.StationParameter))
            {
                if (entity.StationId.ToString() != parameters[FilterParamsNames.StationParameter])
                    return false;
            }

            return true;
        }
    }
}