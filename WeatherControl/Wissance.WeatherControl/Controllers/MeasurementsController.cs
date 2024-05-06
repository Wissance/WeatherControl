using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wissance.WeatherControl.Data.Entity;
using Wissance.WeatherControl.Dto;
using Wissance.WeatherControl.WebApi.Filters;
using Wissance.WeatherControl.WebApi.Managers;
using Wissance.WebApiToolkit.Controllers;

namespace Wissance.WeatherControl.WebApi.Controllers
{
    public class MeasurementsController : BasicCrudController<MeasurementsDto, MeasurementsEntity, int, MeasurementsFilterable>
    {
        public MeasurementsController(MeasurementsManager manager)
        {
            Manager = manager;  // this is for basic operations
            _manager = manager; // this for extended operations
        }

        private MeasurementsManager _manager;
    }
}
