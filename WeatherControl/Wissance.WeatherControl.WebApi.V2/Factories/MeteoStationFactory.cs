using System;
using System.Collections.Generic;
using System.Linq;
using Wissance.WeatherControl.Dto.V2;
using Wissance.WeatherControl.GraphData.Entity;
using Wissance.WeatherControl.WebApi.V2.Helpers;

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

        public static IDictionary<string, object?> Create(MeteoStationDto dto, bool generateId)
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