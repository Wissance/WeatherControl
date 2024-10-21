using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wissance.WeatherControl.Data.Entity;
using Wissance.WeatherControl.Dto;

namespace Wissance.WeatherControl.WebApi.Factory
{
    internal static class MeasurementFactory
    {
        public static MeasurementDto Create(MeasurementEntity entity)
        {
            return new MeasurementDto(entity.Id, entity.SampleDate, entity.Value, null);
        }

        public static MeasurementEntity Create(MeasurementDto dto)
        {
            return new MeasurementEntity()
            {
                Value = dto.Value,
                SampleDate = dto.SampleDate
            };
        }
    }
}
