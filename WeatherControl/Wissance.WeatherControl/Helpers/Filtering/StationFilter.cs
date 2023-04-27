using System.Collections.Generic;
using Wissance.WeatherControl.Data.Entity;

namespace Wissance.WeatherControl.WebApi.Helpers.Filtering
{
    public static class StationFilter
    {
        public static bool Filter(StationEntity entity, IDictionary<string, string> parameters)
        {
            return true;
        }
    }
}