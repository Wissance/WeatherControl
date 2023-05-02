using System;
using System.Collections.Generic;
using System.Linq;
using Wissance.WeatherControl.Data.Entity;
using Wissance.WebApiToolkit.Utils.Extractors;

namespace Wissance.WeatherControl.WebApi.Helpers.Filtering
{
    public static class MeasurementsFilter
    {
        public static bool Filter(MeasurementsEntity entity, IDictionary<string, string> parameters)
        {
            if (parameters.ContainsKey(FromFilterParam))
            {
                Tuple<DateTime, bool> fromVal = ValueExtractor.TryGetVal<DateTime>(parameters[FromFilterParam]);
                if (fromVal.Item2)
                {
                    if (entity.Timestamp < fromVal.Item1)
                        return false;
                }
            }

            if (parameters.ContainsKey(ToFilterParam))
            {
                Tuple<DateTime, bool> toVal = ValueExtractor.TryGetVal<DateTime>(parameters[ToFilterParam]);
                if (toVal.Item2)
                {
                    if (entity.Timestamp > toVal.Item1)
                        return false;
                }
            }

            if (parameters.ContainsKey(StationFilterParam))
            {
                Tuple<int[], bool> stationsVal = ValueExtractor.TryGetArray<int>(parameters[StationFilterParam]);
                if (stationsVal.Item2)
                {
                    if (!stationsVal.Item1.Contains(entity.StationId))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private const string StationFilterParam = "station";
        private const string FromFilterParam = "from";
        private const string ToFilterParam = "to";
    }
}