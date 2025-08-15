using System;
using System.Collections.Generic;
using System.Linq;
using Wissance.WeatherControl.Data.Entity;
using Wissance.WeatherControl.WebApi.Filters;
using Wissance.WebApiToolkit.Core.Utils.Extractors;

namespace Wissance.WeatherControl.WebApi.Helpers.Filtering
{
    public static class MeasurementsFilter
    {
        public static bool Filter(MeasurementEntity entity, IDictionary<string, string> parameters)
        {
            if (parameters.ContainsKey(FilterParamsNames.FromParameter))
            {
                Tuple<DateTime, bool> fromVal = ValueExtractor.TryGetVal<DateTime>(parameters[FilterParamsNames.FromParameter]);
                if (fromVal.Item2)
                {
                    if (entity.SampleDate < fromVal.Item1)
                        return false;
                }
            }

            if (parameters.ContainsKey(FilterParamsNames.ToParameter))
            {
                Tuple<DateTime, bool> toVal = ValueExtractor.TryGetVal<DateTime>(parameters[FilterParamsNames.ToParameter]);
                if (toVal.Item2)
                {
                    if (entity.SampleDate > toVal.Item1)
                        return false;
                }
            }

            if (parameters.ContainsKey(FilterParamsNames.StationParameter))
            {
                Tuple<Guid[], bool> stationsVal = ValueExtractor.TryGetArray<Guid>(parameters[FilterParamsNames.StationParameter]);
                if (stationsVal.Item2)
                {
                    if (!stationsVal.Item1.Contains(entity.Sensor.StationId))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}