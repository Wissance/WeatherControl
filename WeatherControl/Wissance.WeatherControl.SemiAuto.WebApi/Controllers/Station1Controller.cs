using System;
using Wissance.WeatherControl.Common.Automation;
using Wissance.WeatherControl.Data.Entity;
using Wissance.WebApiToolkit.Core.Data;
using Wissance.WebApiToolkit.Core.Managers;

namespace Wissance.WeatherControl.SemiAuto.WebApi.Controllers
{

    public class Station1Controller : GenericController<StationEntity, StationEntity, Guid, EmptyAdditionalFilters>
    {
        public Station1Controller(IModelManager<StationEntity, StationEntity, Guid> manager) : base(manager)
        {
        }
    }
}