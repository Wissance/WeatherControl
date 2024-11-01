using System;
using System.Collections.Generic;
using System.Linq;
using Wissance.WeatherControl.Dto;
using Wissance.WeatherControl.EdgeDb.Data.Entity;
using Wissance.WeatherControl.WebApi.V2.Helpers;

namespace Wissance.WeatherControl.WebApi.V2.Factories
{
    internal static class StationFactory
    {
        public static StationDto Create(StationEntity entity)
        {
            StationDto dto = new StationDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Latitude = entity.Latitude,
                Longitude = entity.Longitude
            };

            if (entity.Sensors.Any())
            {
                dto.Sensors = entity.Sensors.Select(s => SensorFactory.Create(s)).ToArray();
            }

            return dto;
        }

        public static IDictionary<string, object?> Create(StationDto dto, bool generateId)
        {
            IDictionary<string, object?> dict = new Dictionary<string, object?>()
            {
                {"Latitude", dto.Latitude},
                {"Longitude", dto.Longitude},
                {"Sensors", dto.Sensors.Select(s => s.Id).ToArray()}
            };
            
            // TODO(this if for further getting created object)
            dict["id"] = generateId ? Guid.NewGuid() : dto.Id;

            return dict;
        }
    }
}