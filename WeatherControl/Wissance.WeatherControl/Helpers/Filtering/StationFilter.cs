using System.Collections.Generic;
using Wissance.WeatherControl.Data.Entity;
using Wissance.WeatherControl.WebApi.Filters;

namespace Wissance.WeatherControl.WebApi.Helpers.Filtering
{
    public static class StationFilter
    {
        public static bool Filter(StationEntity entity, IDictionary<string, string> parameters)
        {
            if (parameters.ContainsKey(FilterParamsNames.NameParameter))
            {
                return entity.Name.ToLower().Contains(parameters[FilterParamsNames.NameParameter]);
            }

            return true;
        }
    }
}