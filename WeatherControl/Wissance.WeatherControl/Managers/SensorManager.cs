using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Wissance.WeatherControl.Data;
using Wissance.WeatherControl.Data.Entity;
using Wissance.WeatherControl.Dto;
using Wissance.WeatherControl.WebApi.Factory;
using Wissance.WebApiToolkit.Dto;
using Wissance.WebApiToolkit.Managers;

namespace Wissance.WeatherControl.WebApi.Managers
{
    public class SensorManager: EfModelManager<SensorEntity, SensorDto, Guid>
    {
        public SensorManager(ModelContext modelContext, ILoggerFactory loggerFactory) 
            : base(modelContext, null, SensorFactory.Create, loggerFactory)
        {
        }

        public override Task<OperationResultDto<SensorDto>> CreateAsync(SensorDto data)
        {
            return base.CreateAsync(data);
        }

        public override Task<OperationResultDto<SensorDto>> UpdateAsync(Guid id, SensorDto data)
        {
            return base.UpdateAsync(id, data);
        }

        private readonly ModelContext _modelContext;
    }
}