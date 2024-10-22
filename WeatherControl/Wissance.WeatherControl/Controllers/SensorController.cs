using System;
using Wissance.WeatherControl.Data.Entity;
using Wissance.WeatherControl.Dto;
using Wissance.WeatherControl.WebApi.Managers;
using Wissance.WebApiToolkit.Controllers;
using Wissance.WebApiToolkit.Data;

namespace Wissance.WeatherControl.WebApi.Controllers
{
    public class SensorController : BasicCrudController<SensorDto, SensorEntity, Guid, EmptyAdditionalFilters>
    {
        public SensorController(SensorManager manager)
        {
            Manager = manager;  // this is for basic operations
            _manager = manager; // this for extended operations
        }

        private SensorManager _manager;
    }
}