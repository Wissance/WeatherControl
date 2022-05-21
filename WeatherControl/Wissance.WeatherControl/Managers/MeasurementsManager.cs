using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Wissance.WeatherControl.Data;
using Wissance.WeatherControl.Data.Entity;
using Wissance.WeatherControl.Dto;
using Wissance.WebApiToolkit.Managers;

namespace Wissance.WeatherControl.WebApi.Managers
{
    public class MeasurementsManager : ModelManager<MeasurementsEntity, MeasurementsDto, int>
    {
        public MeasurementsManager(ModelContext modelContext, ILoggerFactory loggerFactory) : base(loggerFactory)
        {
            _modelContext = modelContext;
        }

        private readonly ModelContext _modelContext;
    }
}
