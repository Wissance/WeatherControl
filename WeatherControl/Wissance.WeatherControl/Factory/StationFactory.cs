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
        public static StationDto Create (StationEntity entity)
        {
            return new StationDto(entity.Id, entity.Name, entity.Description, entity.Longitude, entity.Latitude);
        }
    }
}
