using Microsoft.AspNetCore.Mvc;
using Wissance.WeatherControl.Data.Entity;
using Wissance.WeatherControl.Dto;
using Wissance.WeatherControl.WebApi.Managers;
using Wissance.WebApiToolkit.Controllers;
using Wissance.WebApiToolkit.Data;

namespace Wissance.WeatherControl.WebApi.Controllers
{
    [ApiController]
    public class StationController : BasicCrudController<StationDto, StationEntity, int, EmptyAdditionalFilters>
    {
        public StationController(StationManager manager)
        {
            Manager = manager;  // this is for basic operations
            _manager = manager; // this for extended operations
        }

        private StationManager _manager;
    }
}
