using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wissance.WeatherControl.Data.Entity;
using Wissance.WeatherControl.Dto;

namespace Wissance.WeatherControl.WebApi.Factory
{
    internal static class StationFactory
    {
        public static StationDto Create(StationEntity entity)
        {
            StationDto dto = new StationDto(entity.Id, entity.Name, entity.Description, entity.Longitude, 
                entity.Latitude, null);
            if (entity.Sensors != null)
            {
                dto.Sensors = entity.Sensors.Select(s => SensorFactory.CreateMin(s)).ToArray();
            }

            return dto;
        }

        public static StationEntity Create(StationDto dto)
        {
            StationEntity entity = new StationEntity()
            {
                Name = dto.Name,
                Description = dto.Description,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
            };

            if (dto.Sensors != null)
            {
                entity.Sensors = dto.Sensors.Select(s => SensorFactory.Create(s)).ToList();
            }

            return entity;
        }
    }
}
