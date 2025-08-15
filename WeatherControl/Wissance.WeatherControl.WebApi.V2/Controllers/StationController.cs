
using System;
using EdgeDB;
using Wissance.WeatherControl.Dto;
using Wissance.WeatherControl.EdgeDb.Data;
using Wissance.WeatherControl.EdgeDb.Data.Entity;
using Wissance.WeatherControl.WebApi.V2.Factories;
using Wissance.WeatherControl.WebApi.V2.Filters;
using Wissance.WeatherControl.WebApi.V2.Managers;
using Wissance.WebApiToolkit.Core.Controllers;

namespace Wissance.WeatherControl.WebApi.V2.Controllers
{
    public class StationController : BasicCrudController<StationDto, StationEntity, Guid, StationFilterable>
    {
        public StationController(EdgeDBClient edgeDbClient)
        {
            Manager = new EdgeDbManager<StationDto, StationEntity, Guid>(ModelType.Station, edgeDbClient,
                StationFactory.Create, StationFactory.Create);
        }
    }
}