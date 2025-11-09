using System;
using System.Collections.Generic;
using System.Linq;
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
    public class MeasurementManager : EfModelManager<ModelContext, MeasurementDto, MeasurementEntity, Guid>
    {
        public MeasurementManager(ModelContext modelContext, ILoggerFactory loggerFactory) 
            : base(modelContext, MeasurementsFilter.Filter, MeasurementFactory.Create, 
                MeasurementFactory.Create, MeasurementFactory.Update, loggerFactory)
        {
        }
    }
}
