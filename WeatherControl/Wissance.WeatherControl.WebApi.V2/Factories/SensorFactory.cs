using System.Collections.Generic;
using System.Linq;
using Wissance.WeatherControl.Dto.V2;
using Wissance.WeatherControl.GraphData.Entity;

namespace Wissance.WeatherControl.WebApi.V2.Factories
{
    public static class SensorFactory
    {
        public static SensorDto Create(SensorEntity entity)
        {
            SensorDto dto = new SensorDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                Latitude = entity.Latitude,
                Longitude = entity.Longitude
            };

            if (entity.Measurements.Any())
            {
                dto.Measurements = entity.Measurements.Select(m => MeasurementFactory.Create((m))).ToList();
            }

            return dto;
        }
        
        public static IDictionary<string, object?> Create(SensorDto dto, bool generateId)
        {
            return new Dictionary<string, object?>()
            {
                
            };
        }
    }
}