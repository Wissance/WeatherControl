using System.Collections.Generic;
using Wissance.WeatherControl.Data.Entity;

namespace Wissance.WeatherControl.WebApi.Helpers.Filtering
{
    public static class MeasurementsFilter
    {
        public static bool Filter(MeasurementsEntity entity, IDictionary<string, string> parameters)
        {
            return true;
        }
    }
}