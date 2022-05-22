using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Wissance.WeatherControl.Data;
using Wissance.WeatherControl.Data.Entity;
using Wissance.WeatherControl.Dto;
using Wissance.WeatherControl.WebApi.Factory;
using Wissance.WebApiToolkit.Dto;
using Wissance.WebApiToolkit.Managers;

namespace Wissance.WeatherControl.WebApi.Managers
{
    public class MeasurementsManager : ModelManager<MeasurementsEntity, MeasurementsDto, int>
    {
        public MeasurementsManager(ModelContext modelContext, ILoggerFactory loggerFactory) : base(loggerFactory)
        {
            _modelContext = modelContext;
        }

        public override async Task<OperationResultDto<IList<MeasurementsDto>>> GetAsync(int page, int size)
        {
            return await GetAsync<int>(_modelContext.Measurements, page, size, null, null, MeasurementsFactory.Create);
        }

        public override async Task<OperationResultDto<MeasurementsDto>> GetByIdAsync(int id)
        {
            return await GetAsync(_modelContext.Measurements, id, MeasurementsFactory.Create);
        }

        public override async Task<OperationResultDto<MeasurementsDto>> CreateAsync(MeasurementsDto data)
        {
            try
            {
                MeasurementsEntity entity = MeasurementsFactory.Create(data);
                await _modelContext.Measurements.AddAsync(entity);
                int result = await _modelContext.SaveChangesAsync();
                if (result >= 0)
                {
                    return new OperationResultDto<MeasurementsDto>(true, (int)HttpStatusCode.Created, null, MeasurementsFactory.Create(entity));
                }
                return new OperationResultDto<MeasurementsDto>(false, (int)HttpStatusCode.InternalServerError, "An unknown error occurred during measurements creation", null);
            }
            catch (Exception e)
            {
                return new OperationResultDto<MeasurementsDto>(false, (int)HttpStatusCode.InternalServerError, $"An error occurred during measurements creation: {e.Message}", null);
            }
            
        }

        public override async Task<OperationResultDto<MeasurementsDto>> UpdateAsync(int id, MeasurementsDto data)
        {
            try
            {
                return await base.UpdateAsync(id, data);
            }
            catch (Exception e)
            {
                return new OperationResultDto<MeasurementsDto>(false, (int)HttpStatusCode.InternalServerError, $"An error occurred during measurements update: {e.Message}", null);
            }

        public override async Task<OperationResultDto<bool>> DeleteAsync(int id)
        {
            return await DeleteAsync(_modelContext, _modelContext.Measurements, id);
        }

        private readonly ModelContext _modelContext;
    }
}
