using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Wissance.WebApiToolkit.Core.Data;

namespace Wissance.WeatherControl.WebApi.Filters
{
    public class StationFilterable : IReadFilterable
    {
        public IDictionary<string, string> SelectFilters()
        {
            IDictionary<string, string> additionalFilters = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(Name))
            {
                additionalFilters.Add(FilterParamsNames.NameParameter, Name.ToLower());
            }

            return additionalFilters;
        }
        
        [FromQuery(Name = FilterParamsNames.NameParameter)]
        public string Name { get; set; }
    }
}