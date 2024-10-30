using System;
using System.Collections.Generic;
using System.Text;
using Wissance.WeatherControl.Dto;
using Wissance.WeatherControl.EdgeDb.Data.Entity;
using Wissance.WeatherControl.WebApi.V2.Helpers;

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
                // MeasureUnitId = entity.Unit.Id,
                SensorId = entity.Sensor.Id,
                Value = entity.Value
            };
        }
        
        
        public static IDictionary<string, object?> Create(MeasurementDto dto, bool generateId)
        {
            IDictionary<string, object?> dict = new Dictionary<string, object?>()
            {
                {"SampleDate", dto.SampleDate},
                {"Value", dto.Value.ToString()},
                {"SensorId", dto.SensorId}
            };
            
            // TODO(this if for further getting created object)
            dict["id"] = generateId ? Guid.NewGuid() : dto.Id;

            return dict;
        }
        
    }
}