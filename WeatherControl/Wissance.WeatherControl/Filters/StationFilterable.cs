using System.Collections.Generic;
using Wissance.WebApiToolkit.Data;

namespace Wissance.WeatherControl.WebApi.Filters
{
    public class StationFilterable : IReadFilterable
    {
        public IDictionary<string, string> SelectFilters()
        {
            IDictionary<string, string> additionalFilters = new Dictionary<string, string>();

            return additionalFilters;
        }
    }
}