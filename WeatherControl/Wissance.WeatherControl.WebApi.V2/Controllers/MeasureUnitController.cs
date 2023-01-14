using System;
using Wissance.WebApiToolkit.Controllers;
using Wissance.WeatherControl.Dto.V2;
using Wissance.WeatherControl.GraphData.Entity;
using Wissance.WeatherControl.WebApi.V2.Managers;
using Wissance.WebApiToolkit.Managers;

namespace Wissance.WeatherControl.WebApi.V2.Controllers
{
    public class MeasureUnitController : BasicReadController<MeasureUnitDto, MeasureUnitEntity, Guid>
    {
        public MeasureUnitController(EdgeDbManager<MeasureUnitDto, MeasureUnitEntity, Guid> manager)
        {
            Manager = manager;
        }
        
        //private IModelManager<MeasureUnitDto, MeasureUnitEntity, Guid> _
    }
}
