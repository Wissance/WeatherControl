using System;
using Microsoft.AspNetCore.Mvc;
using Wissance.WeatherControl.Data.Entity;
using Wissance.WeatherControl.Dto;
using Wissance.WeatherControl.WebApi.Filters;
using Wissance.WeatherControl.WebApi.Managers;
using Wissance.WebApiToolkit.Controllers;
using Wissance.WebApiToolkit.Data;

namespace Wissance.WeatherControl.WebApi.Controllers
{
    public class StationController : BasicCrudController<StationDto, StationEntity, Guid, StationFilterable>
    {
        public StationController(StationManager manager)
        {
            Manager = manager;  // this is for basic operations
            _manager = manager; // this for extended operations
        }

        private StationManager _manager;
    }
}
