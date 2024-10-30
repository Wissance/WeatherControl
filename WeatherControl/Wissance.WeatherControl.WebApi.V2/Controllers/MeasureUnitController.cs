using System;
using EdgeDB;
using Wissance.WebApiToolkit.Controllers;
using Wissance.WeatherControl.Dto;
using Wissance.WeatherControl.EdgeDb.Data;
using Wissance.WeatherControl.EdgeDb.Data.Entity;
using Wissance.WeatherControl.WebApi.V2.Factories;
using Wissance.WeatherControl.WebApi.V2.Filters;
using Wissance.WeatherControl.WebApi.V2.Managers;

namespace Wissance.WeatherControl.WebApi.V2.Controllers
{
    public class MeasureUnitController : BasicReadController<MeasureUnitDto, MeasureUnitEntity, Guid, 
        MeasureUnitFilterable>
    {
        public MeasureUnitController(EdgeDBClient edgeDbClient)
        {
            Manager = new EdgeDbManager<MeasureUnitDto, MeasureUnitEntity, Guid>(ModelType.MeasureUnit, edgeDbClient,
                MeasureUnitFactory.Create, null);
        }
        
    }
}
