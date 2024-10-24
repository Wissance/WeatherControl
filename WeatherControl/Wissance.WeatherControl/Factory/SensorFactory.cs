using Wissance.WeatherControl.Data.Entity;
using Wissance.WeatherControl.Dto;

namespace Wissance.WeatherControl.WebApi.Factory
{
    internal static class SensorFactory
    {
        public static SensorEntity Create(SensorDto dto)
        {
            SensorEntity entity = new SensorEntity()
            {
                Name = dto.Name,
                Description = dto.Description,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                StationId = dto.StationId,
                MeasureUnitId = dto.MeasureUnitId
            };
            return entity;
        }

        public static SensorEntity Create(SensorMinDataDto dto)
        {
            SensorEntity entity = new SensorEntity()
            {
                Name = dto.Name,
                Description = dto.Description,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                StationId = dto.StationId,
                MeasureUnitId = dto.MeasureUnitId
            };
            
            return entity;
        }

        public static SensorDto Create(SensorEntity entity)
        {
            return new SensorDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Latitude = entity.Latitude,
                Longitude = entity.Longitude,
                StationId = entity.StationId,
                MeasureUnitId = entity.MeasureUnitId
            };
        }
        
        public static SensorMinDataDto CreateMin(SensorEntity entity)
        {
            return new SensorMinDataDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Latitude = entity.Latitude,
                Longitude = entity.Longitude,
                StationId = entity.StationId,
                MeasureUnitId = entity.MeasureUnitId
            };
        }
    }
}