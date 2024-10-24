using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Wissance.WebApiToolkit.Data;

namespace Wissance.WeatherControl.WebApi.Filters
{
    public class SensorFilterable : IReadFilterable
    {
        public IDictionary<string, string> SelectFilters()
        {
            IDictionary<string, string> filterParams = new Dictionary<string, string>();
            if (Station.HasValue)
            {
                filterParams.Add(new KeyValuePair<string, string>(FilterParamsNames.StationParameter, Station.Value.ToString()));
            }
            
            if (MeasureUnit.HasValue)
            {
                filterParams.Add(new KeyValuePair<string, string>(FilterParamsNames.MeasureUnitParameter, MeasureUnit.Value.ToString()));
            }

            return filterParams;
        }
        
        [FromQuery(Name = FilterParamsNames.StationParameter)]
        public Guid? Station { get; set; }
        
        [FromQuery(Name = FilterParamsNames.MeasureUnitParameter)]
        public Guid? MeasureUnit { get; set; }
    }
}