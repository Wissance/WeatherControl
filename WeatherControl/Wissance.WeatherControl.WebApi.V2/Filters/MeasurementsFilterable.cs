using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Wissance.WebApiToolkit.Data;

namespace Wissance.WeatherControl.WebApi.V2.Filters
{
    public class MeasurementsFilterable : IReadFilterable
    {
        public IDictionary<string, string> SelectFilters()
        {
            IDictionary<string, string> additionalFilters = new Dictionary<string, string>();
            if (From.HasValue)
            {
                additionalFilters.Add(FilterParamsNames.FromParameter, From.ToString());
            }
            if (To.HasValue)
            {
                additionalFilters.Add(FilterParamsNames.ToParameter, To.ToString());
            }

            if (Station != null)
            {
                additionalFilters.Add(FilterParamsNames.StationParameter, string.Join(",", Station));
            }

            return additionalFilters;
        }
        
        [FromQuery(Name = "from")] 
        public DateTimeOffset? From { get; set; }
        
        [FromQuery(Name = "to")] 
        public DateTimeOffset? To { get; set; }
        
        [FromQuery(Name = "station")] 
        public Guid[] Station { get; set; }
    }
}