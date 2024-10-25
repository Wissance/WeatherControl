
using System;
using EdgeDB;
using Wissance.WeatherControl.Dto.V2;
using Wissance.WeatherControl.EdgeDb.Data;
using Wissance.WeatherControl.EdgeDb.Data.Entity;
using Wissance.WeatherControl.WebApi.V2.Factories;
using Wissance.WeatherControl.WebApi.V2.Managers;
using Wissance.WebApiToolkit.Controllers;
using Wissance.WebApiToolkit.Data;

namespace Wissance.WeatherControl.WebApi.V2.Controllers
{
    public class MeteoStationController : BasicCrudController<MeteoStationDto, StationEntity, Guid, EmptyAdditionalFilters>
    {
        public MeteoStationController(EdgeDBClient edgeDbClient)
        {
            Manager = new EdgeDbManager<MeteoStationDto, StationEntity, Guid>(ModelType.Station, edgeDbClient,
                MeteoStationFactory.Create, MeteoStationFactory.Create);
        }
    }
}