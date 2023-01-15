
using System;
using Wissance.WeatherControl.Dto.V2;
using Wissance.WeatherControl.GraphData.Entity;
using Wissance.WebApiToolkit.Controllers;

namespace Wissance.WeatherControl.WebApi.V2.Controllers
{
    public class MeteoStationController : BasicCrudController<MeteoStationDto, MeteoStationEntity, Guid>
    {

    }
}