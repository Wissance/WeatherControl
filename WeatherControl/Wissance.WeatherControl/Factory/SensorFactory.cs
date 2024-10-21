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
                // StationId = dto.
                // MeasureUnitId = dto.
            };
            return entity;
        }

        public static SensorEntity Create(SensorMinData dto)
        {
            SensorEntity entity = new SensorEntity()
            {
                
            };
            
            return entity;
        }
    }
}