using System.Linq;
using Wissance.WeatherControl.Dto.V2;
using Wissance.WeatherControl.GraphData.Entity;

namespace Wissance.WeatherControl.WebApi.V2.Factories
{
    public static class MeteoStationFactory
    {
        public static MeteoStationDto Create(MeteoStationEntity entity)
        {
            MeteoStationDto dto = new MeteoStationDto()
            {
                Id = entity.Id,
                Latitude = entity.Latitude,
                Longitude = entity.Longitude
            };

            if (entity.Sensors.Any())
            {
                dto.Sensors = entity.Sensors.Select(s => SensorFactory.Create(s)).ToList();
            }

            return dto;
        }
    }
}