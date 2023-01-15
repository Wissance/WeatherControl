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
    public class SensorController : BasicCrudController<SensorDto, SensorEntity, Guid>
    {
        public SensorController(EdgeDBClient edgeDbClient)
        {
            Manager = new EdgeDbManager<SensorDto, SensorEntity, Guid>(ModelType.Sensor, edgeDbClient,
                SensorFactory.Create);
        }
    }
}