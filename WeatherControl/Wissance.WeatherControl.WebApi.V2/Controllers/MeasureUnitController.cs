using System;
using EdgeDB;
using Wissance.WebApiToolkit.Controllers;
using Wissance.WeatherControl.Dto.V2;
using Wissance.WeatherControl.GraphData;
using Wissance.WeatherControl.GraphData.Entity;
using Wissance.WeatherControl.WebApi.V2.Factories;
using Wissance.WeatherControl.WebApi.V2.Managers;
using Wissance.WebApiToolkit.Managers;

namespace Wissance.WeatherControl.WebApi.V2.Controllers
{
    public class MeasureUnitController : BasicReadController<MeasureUnitDto, MeasureUnitEntity, Guid>
    {
        public MeasureUnitController(EdgeDBClient edgeDbClient)
        {
            Manager = new EdgeDbManager<MeasureUnitDto, MeasureUnitEntity, Guid>(ModelType.MeasureUnit, edgeDbClient,
                MeasureUnitFactory.Create);
        }
        
    }
}
