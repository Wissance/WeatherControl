using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Wissance.WeatherControl.Data;
using Wissance.WeatherControl.Data.Entity;
using Wissance.WeatherControl.Dto;
using Wissance.WeatherControl.WebApi.Factory;
using Wissance.WebApiToolkit.Dto;
using Wissance.WebApiToolkit.Ef.Managers;

namespace Wissance.WeatherControl.WebApi.Managers
{
    public class MeasureUnitManager : EfModelManager<MeasureUnitDto, MeasureUnitEntity, Guid>
    {
        public MeasureUnitManager(ModelContext modelContext, ILoggerFactory loggerFactory) 
            : base(modelContext, null, MeasureUnitFactory.Create, MeasureUnitFactory.Create, 
                MeasureUnitFactory.Update, loggerFactory)
        {
        }
    }
}