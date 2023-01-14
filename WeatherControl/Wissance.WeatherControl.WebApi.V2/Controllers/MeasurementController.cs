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
    public class MeasurementController : BasicCrudController<MeasurementDto, MeasurementEntity, Guid>
    {
        public MeasurementController(EdgeDBClient edgeDbClient)
        {
            Manager = new EdgeDbManager<MeasurementDto, MeasurementEntity, Guid>(ModelType.Measurement, edgeDbClient,
                MeasurementFactory.Create);
        }
    }
}