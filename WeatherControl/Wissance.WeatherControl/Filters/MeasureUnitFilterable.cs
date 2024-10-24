using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Wissance.WebApiToolkit.Data;

namespace Wissance.WeatherControl.WebApi.Filters
{
    public class MeasureUnitFilterable : IReadFilterable
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