using System;
using System.Collections.Generic;
using Wissance.WeatherControl.Dto.V2;
using Wissance.WeatherControl.GraphData.Entity;

namespace Wissance.WeatherControl.WebApi.V2.Factories
{
    public static class MeasurementFactory
    {
        public static MeasurementDto Create(MeasurementEntity entity)
        {
            return new MeasurementDto()
            {
                Id = entity.Id,
                SampleDate = entity.SampleDate,
                MeasureUnitId = entity.Unit.Id,
                SensorId = entity.Sensor.Id,
                Value = entity.Value
            };
        }
        
        
        public static IDictionary<string, object?> Create(MeasurementDto dto)
        {
            return new Dictionary<string, object?>()
            {
                {"id", Guid.NewGuid()},  // TODO(this if for further getting created object)
                {"SampleDate", dto.SampleDate},
                {"Value", dto.Value.ToString()},
                {"MeasureUnitId", dto.MeasureUnitId},
                {"SensorId", dto.SensorId}
            };
        }
    }
}