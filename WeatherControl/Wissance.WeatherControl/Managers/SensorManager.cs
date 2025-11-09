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
using Wissance.WeatherControl.WebApi.Helpers.Filtering;
using Wissance.WebApiToolkit.Dto;
using Wissance.WebApiToolkit.Ef.Managers;

namespace Wissance.WeatherControl.WebApi.Managers
{
    public class SensorManager: EfModelManager<ModelContext, SensorDto, SensorEntity,  Guid>
    {
        public SensorManager(ModelContext modelContext, ILoggerFactory loggerFactory) 
            : base(modelContext, SensorFilter.Filter, SensorFactory.Create, SensorFactory.Create, 
                SensorFactory.Update, loggerFactory)
        {
            
        }
    }
}