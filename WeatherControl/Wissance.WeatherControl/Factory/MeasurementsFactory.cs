using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wissance.WeatherControl.Data.Entity;
using Wissance.WeatherControl.Dto;

namespace Wissance.WeatherControl.WebApi.Factory
{
    internal static class MeasurementsFactory
    {
        public static MeasurementsDto Create(MeasurementsEntity entity)
        {
            return new MeasurementsDto(entity.Id, entity.Timestamp, entity.Temperature, entity.Pressure, entity.Humidity,
                                       entity.WindSpeed, entity.StationId);
        }
    }
}
