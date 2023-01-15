
using System;
using EdgeDB;
using Wissance.WeatherControl.Dto.V2;
using Wissance.WeatherControl.GraphData;
using Wissance.WeatherControl.GraphData.Entity;
using Wissance.WeatherControl.WebApi.V2.Factories;
using Wissance.WeatherControl.WebApi.V2.Managers;
using Wissance.WebApiToolkit.Controllers;

namespace Wissance.WeatherControl.WebApi.V2.Controllers
{
    public class MeteoStationController : BasicCrudController<MeteoStationDto, MeteoStationEntity, Guid>
    {
        public MeteoStationController(EdgeDBClient edgeDbClient)
        {
            Manager = new EdgeDbManager<MeteoStationDto, MeteoStationEntity, Guid>(ModelType.MeteoStation, edgeDbClient,
                MeteoStationFactory.Create, MeteoStationFactory.Create);
        }
    }
}