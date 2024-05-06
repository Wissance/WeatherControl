using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Wissance.WebApiToolkit.Data;

namespace Wissance.WeatherControl.WebApi.Filters
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
        
        [FromQuery(Name = "from")] public DateTime? From { get; set; }
        [FromQuery(Name = "to")] public DateTime? To { get; set; }
        [FromQuery(Name = "station")] public int[] Station { get; set; }
    }
}