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
    public class MeasurementSeriesController : BasicBulkCrudController<MeasurementDto, MeasurementEntity, Guid, 
        MeasurementsFilterable>
    {
        public MeasurementSeriesController(EdgeDBClient edgeDbClient)
        {
            Manager = new EdgeDbManager<MeasurementDto, MeasurementEntity, Guid>(ModelType.Measurement, edgeDbClient,
                MeasurementFactory.Create, MeasurementFactory.Create);
        }
    }
}