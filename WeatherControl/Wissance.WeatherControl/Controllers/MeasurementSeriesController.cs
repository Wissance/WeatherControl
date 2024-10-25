using System;
using Wissance.WeatherControl.Data.Entity;
using Wissance.WeatherControl.Dto;
using Wissance.WeatherControl.WebApi.Filters;
using Wissance.WeatherControl.WebApi.Managers;
using Wissance.WebApiToolkit.Controllers;

namespace Wissance.WeatherControl.WebApi.Controllers
{
    public class MeasurementSeriesController: BasicBulkCrudController<MeasurementDto, MeasurementEntity, Guid, 
        MeasurementsFilterable>
    {
        public MeasurementSeriesController(MeasurementManager manager)
        {
            Manager = manager;  // this is for basic operations
            _manager = manager; // this for extended operations
        }

        private MeasurementManager _manager;
    }
}