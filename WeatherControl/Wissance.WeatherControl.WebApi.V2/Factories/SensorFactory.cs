using System;
using System.Collections.Generic;
using System.Linq;
using Wissance.WeatherControl.Dto.V2;
using Wissance.WeatherControl.GraphData.Entity;
using Wissance.WeatherControl.WebApi.V2.Helpers;

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
        
        public static IDictionary<string, object?> Create(SensorDto dto, bool generateId, string suffix = null)
        {
            IDictionary<string, object?> dict = new Dictionary<string, object?>()
            {
                {ParamsSuffixAppender.Append("Name", suffix), dto.Name},
                {ParamsSuffixAppender.Append("Latitude", suffix), dto.Latitude},
                {ParamsSuffixAppender.Append("Longitude", suffix), dto.Longitude},
                {ParamsSuffixAppender.Append("Measurements", suffix), dto.Measurements.Where(m => m.Id.HasValue)
                    .Select(m => m.Id.Value).ToArray()}
            };
            
            // TODO(this if for further getting created object)
            dict[ParamsSuffixAppender.Append("id", suffix)] = generateId ? Guid.NewGuid() : dto.Id;

            return dict;
        }
    }
}