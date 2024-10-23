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
    public class MeasureUnitManager : EfModelManager<MeasureUnitEntity, MeasureUnitDto, Guid>
    {
        public MeasureUnitManager(ModelContext modelContext, ILoggerFactory loggerFactory) 
            : base(modelContext, null, MeasureUnitFactory.Create, loggerFactory)
        {
            _modelContext = modelContext;
        }

        public override Task<OperationResultDto<MeasureUnitDto>> CreateAsync(MeasureUnitDto data)
        {
            return base.CreateAsync(data);
        }

        public override Task<OperationResultDto<MeasureUnitDto>> UpdateAsync(Guid id, MeasureUnitDto data)
        {
            return base.UpdateAsync(id, data);
        }

        private readonly ModelContext _modelContext;
    }
}