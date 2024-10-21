using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wissance.WeatherControl.Dto;
using Wissance.WeatherControl.WebApi.Filters;
using Wissance.WeatherControl.WebApi.Managers;
using Wissance.WebApiToolkit.Controllers;

namespace Wissance.WeatherControl.WebApi.Controllers
{
    public class MeasurementsController : BasicCrudController<MeasurementDto, MeasurementEntity, int, 
        MeasurementsFilterable>
    {
        public MeasurementsController(MeasurementManager manager)
        {
            Manager = manager;  // this is for basic operations
            _manager = manager; // this for extended operations
        }

        private MeasurementManager _manager;
    }
}
