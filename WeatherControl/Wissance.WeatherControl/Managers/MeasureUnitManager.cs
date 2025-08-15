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
                null, loggerFactory)
        {
            _modelContext = modelContext;
        }

        /*public override async Task<OperationResultDto<MeasureUnitDto>> CreateAsync(MeasureUnitDto data)
        {
            try
            {
                MeasureUnitEntity entity = MeasureUnitFactory.Create(data);
                await _modelContext.MeasureUnits.AddAsync(entity);
                int result = await _modelContext.SaveChangesAsync();
                if (result >= 0)
                {
                    return new OperationResultDto<MeasureUnitDto>(true, (int) HttpStatusCode.Created, string.Empty,
                        MeasureUnitFactory.Create(entity));
                }
                return new OperationResultDto<MeasureUnitDto>(false, (int) HttpStatusCode.InternalServerError,
                    "An unknown error occurred during \"MeasureUnit\" create", null);
            }
            catch (Exception e)
            {
                return new OperationResultDto<MeasureUnitDto>(false, (int) HttpStatusCode.InternalServerError,
                    $"An error occurred during \"MeasureUnit\" create: {e.Message}", null);
            }
        }*/

        public override async Task<OperationResultDto<MeasureUnitDto>> UpdateAsync(Guid id, MeasureUnitDto data)
        {
            try
            {
                MeasureUnitEntity existingMeasureUnit = await _modelContext.MeasureUnits.FirstOrDefaultAsync(m => m.Id == id);
                if (existingMeasureUnit == null)
                {
                    return new OperationResultDto<MeasureUnitDto>(false, (int) HttpStatusCode.NotFound,
                        $"An error occurred during \"MeasureUnit\" update, an object with id:\":{id}\" does n't exist", null);
                }

                existingMeasureUnit.Name = data.Name;
                existingMeasureUnit.Description = data.Description;
                existingMeasureUnit.Abbreviation = data.Abbreviation;

                int result = await _modelContext.SaveChangesAsync();
                if (result >= 0)
                {
                    return new OperationResultDto<MeasureUnitDto>(true, (int) HttpStatusCode.OK, string.Empty,
                        MeasureUnitFactory.Create(existingMeasureUnit));
                }
                return new OperationResultDto<MeasureUnitDto>(false, (int) HttpStatusCode.InternalServerError,
                    "An unknown error occurred during \"MeasureUnit\" update", null);
            }
            catch (Exception e)
            {
                return new OperationResultDto<MeasureUnitDto>(false, (int) HttpStatusCode.InternalServerError,
                    $"An error occurred during \"MeasureUnit\" update: {e.Message}", null);
            }
        }

        private readonly ModelContext _modelContext;
    }
}