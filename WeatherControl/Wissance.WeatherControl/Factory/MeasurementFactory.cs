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
            return new MeasurementDto(entity.Id, entity.SampleDate, entity.Value, entity.SensorId);
        }

        public static MeasurementEntity Create(MeasurementDto dto)
        {
            return new MeasurementEntity()
            {
                Value = dto.Value,
                SampleDate = dto.SampleDate,
                SensorId = dto.SensorId.HasValue ? dto.SensorId.Value : Guid.Empty
            };
        }

        public static void Update(MeasurementDto data, Guid id, MeasurementEntity entity)
        {
            entity.Value = data.Value;
            entity.SampleDate = data.SampleDate;
        }
    }
}
