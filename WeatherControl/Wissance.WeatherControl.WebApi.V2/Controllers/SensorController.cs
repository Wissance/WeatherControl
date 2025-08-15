using System;
using EdgeDB;
using Wissance.WeatherControl.Dto;
using Wissance.WeatherControl.EdgeDb.Data;
using Wissance.WeatherControl.EdgeDb.Data.Entity;
using Wissance.WeatherControl.WebApi.V2.Factories;
using Wissance.WeatherControl.WebApi.V2.Filters;
using Wissance.WeatherControl.WebApi.V2.Managers;
using Wissance.WebApiToolkit.Core.Controllers;
using Wissance.WebApiToolkit.Data;

namespace Wissance.WeatherControl.WebApi.V2.Controllers
{
    public class SensorController : BasicCrudController<SensorDto, SensorEntity, Guid, SensorFilterable>
    {
        public SensorController(EdgeDBClient edgeDbClient)
        {
            Manager = new EdgeDbManager<SensorDto, SensorEntity, Guid>(ModelType.Sensor, edgeDbClient,
                SensorFactory.Create, SensorFactory.Create);
        }
    }
}