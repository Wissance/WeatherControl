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
using Wissance.WebApiToolkit.Dto;
using Wissance.WebApiToolkit.Managers;

namespace Wissance.WeatherControl.WebApi.Managers
{
    public class StationManager : EfModelManager<StationEntity, StationDto, int>
    {
        public StationManager(ModelContext modelContext, ILoggerFactory loggerFactory) : base(modelContext, StationFactory.Create, loggerFactory)
        {
            _modelContext = modelContext;
        }

        /*public override async Task<OperationResultDto<IList<StationDto>>> GetAsync(int page, int size)
        {
            return await GetAsync<int>(_modelContext.Stations, page, size, null, null, StationFactory.Create);
        }

        public override async Task<OperationResultDto<StationDto>> GetByIdAsync(int id)
        {
            return await GetAsync(_modelContext.Stations, id, StationFactory.Create);
        }*/

        public override async Task<OperationResultDto<StationDto>> CreateAsync(StationDto data)
        {
            try
            {
                StationEntity entity = StationFactory.Create(data);
                await _modelContext.Stations.AddAsync(entity);
                int result = await _modelContext.SaveChangesAsync();
                if (result >= 0)
                {
                    return new OperationResultDto<StationDto>(true, (int)HttpStatusCode.Created, null, StationFactory.Create(entity));
                }
                return new OperationResultDto<StationDto>(false, (int)HttpStatusCode.InternalServerError, "An unknown error occurred during station creation", null);

            }
            catch (Exception e)
            {
                return new OperationResultDto<StationDto>(false, (int)HttpStatusCode.InternalServerError, $"An error occurred during station creation: {e.Message}", null);
            }
        }

        public override async Task<OperationResultDto<StationDto>> UpdateAsync(int id, StationDto data)
        {
            try
            {
                StationEntity entity = StationFactory.Create(data);
                StationEntity existingEntity = await _modelContext.Stations.FirstOrDefaultAsync(s => s.Id == id);
                if (existingEntity == null)
                {
                    return new OperationResultDto<StationDto>(false, (int)HttpStatusCode.NotFound, $"Station with id: {id} does not exists", null);
                }

                // Copy only name, description and positions, create measurements if necessary from MeasurementsManager
                existingEntity.Name = entity.Name;
                existingEntity.Description = existingEntity.Description;
                existingEntity.Latitude = existingEntity.Latitude;
                existingEntity.Longitude = existingEntity.Longitude;
                int result = await _modelContext.SaveChangesAsync();
                if (result >= 0)
                {
                    return new OperationResultDto<StationDto>(true, (int)HttpStatusCode.OK, null, StationFactory.Create(entity));
                }
                return new OperationResultDto<StationDto>(false, (int)HttpStatusCode.InternalServerError, "An unknown error occurred during station update", null);

            }
            catch (Exception e)
            {
                return new OperationResultDto<StationDto>(false, (int)HttpStatusCode.InternalServerError, $"An error occurred during station update: {e.Message}", null);
            }
            
        }

        /*public override async Task<OperationResultDto<bool>> DeleteAsync(int id)
        {
            return await DeleteAsync(_modelContext, _modelContext.Stations, id);
        }*/

        private readonly ModelContext _modelContext;
    }
}
