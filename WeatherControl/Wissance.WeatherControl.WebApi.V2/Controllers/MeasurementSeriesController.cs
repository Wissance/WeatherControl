using System;
using EdgeDB;
using Wissance.WeatherControl.Dto.V2;
using Wissance.WeatherControl.GraphData;
using Wissance.WeatherControl.GraphData.Entity;
using Wissance.WeatherControl.WebApi.V2.Factories;
using Wissance.WeatherControl.WebApi.V2.Managers;
using Wissance.WebApiToolkit.Controllers;
using Wissance.WebApiToolkit.Data;

namespace Wissance.WeatherControl.WebApi.V2.Controllers
{
    public class MeasurementSeriesController : BasicBulkCrudController<MeasurementDto, MeasurementEntity, Guid, EmptyAdditionalFilters>
    {
        public MeasurementSeriesController(EdgeDBClient edgeDbClient)
        {
            Manager = new EdgeDbManager<MeasurementDto, MeasurementEntity, Guid>(ModelType.Measurement, edgeDbClient,
                MeasurementFactory.Create, MeasurementFactory.Create);
        }
    }
}