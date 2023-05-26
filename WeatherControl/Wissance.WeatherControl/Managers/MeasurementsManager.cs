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
using Wissance.WebApiToolkit.Managers;

namespace Wissance.WeatherControl.WebApi.Managers
{
    public class MeasurementsManager : EfModelManager<MeasurementsEntity, MeasurementsDto, int>
    {
        public MeasurementsManager(ModelContext modelContext, ILoggerFactory loggerFactory) 
            : base(modelContext, MeasurementsFilter.Filter, MeasurementsFactory.Create, loggerFactory)
        {
            _modelContext = modelContext;
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

        public override async Task<OperationResultDto<MeasurementsDto[]>> BulkCreateAsync(MeasurementsDto[] data)
        {
            try
            {
                IList<MeasurementsEntity> measurements = data.Select(d => MeasurementsFactory.Create(d)).ToList();
                await _modelContext.Measurements.AddRangeAsync(measurements);
                int result = await _modelContext.SaveChangesAsync();
                if (result >= 0)
                {
                    return new OperationResultDto<MeasurementsDto[]>(true, (int)HttpStatusCode.Created, null,
                        measurements.Select(m => MeasurementsFactory.Create(m)).ToArray());
                }
                return new OperationResultDto<MeasurementsDto[]>(false, (int)HttpStatusCode.InternalServerError, "An unknown error occurred during measurements creation", null);
            }
            catch (Exception e)
            {
                return new OperationResultDto<MeasurementsDto[]>(false, (int)HttpStatusCode.InternalServerError,
                    $"An error occurred during measurements update: {e.Message}", null);
            }
        }

        public override async Task<OperationResultDto<MeasurementsDto>> UpdateAsync(int id, MeasurementsDto data)
        {
            try
            {
                MeasurementsEntity entity = MeasurementsFactory.Create(data);
                MeasurementsEntity existingEntity = await _modelContext.Measurements.FirstOrDefaultAsync(m => m.Id == id);
                if (existingEntity == null)
                {
                    return new OperationResultDto<MeasurementsDto>(false, (int)HttpStatusCode.NotFound, $"Measurements with id: {id} does not exists", null);
                }

                existingEntity.Temperature = entity.Temperature;
                existingEntity.Pressure = entity.Pressure;
                existingEntity.Humidity = entity.Humidity;
                existingEntity.WindSpeed = entity.WindSpeed;
                existingEntity.Timestamp = entity.Timestamp;
                int result = await _modelContext.SaveChangesAsync();
                if (result >= 0)
                {
                    return new OperationResultDto<MeasurementsDto>(true, (int)HttpStatusCode.OK, null, MeasurementsFactory.Create(existingEntity));
                }
                return new OperationResultDto<MeasurementsDto>(false, (int)HttpStatusCode.InternalServerError, $"An unknown error occurred during measurements update", null);
            }
            catch (Exception e)
            {
                return new OperationResultDto<MeasurementsDto>(false, (int)HttpStatusCode.InternalServerError,
                    $"An error occurred during measurements update: {e.Message}", null);
            }
        }
        
        public override async Task<OperationResultDto<MeasurementsDto[]>> BulkUpdateAsync(MeasurementsDto[] data)
        {
            try
            {
                IList<MeasurementsEntity> measurementsToUpdate = await _modelContext.Measurements.Where(m => data.Any(di => di.Id == m.Id))
                                                                                          .ToListAsync();
                foreach (MeasurementsEntity measurements in measurementsToUpdate)
                {
                    MeasurementsDto measurementsDto = data.First(m => m.Id == measurements.Id);
                    measurements.Temperature = measurementsDto.Temperature;
                    measurements.Pressure = measurementsDto.Pressure;
                    measurements.Humidity = measurementsDto.Humidity;
                    measurements.WindSpeed = measurementsDto.WindSpeed;
                    measurements.Timestamp = measurementsDto.Timestamp;
                }
                int result = await _modelContext.SaveChangesAsync();
                if (result >= 0)
                {
                    return new OperationResultDto<MeasurementsDto[]>(true, (int)HttpStatusCode.OK, null,
                        measurementsToUpdate.Select(m => MeasurementsFactory.Create(m)).ToArray());
                }
                return new OperationResultDto<MeasurementsDto[]>(false, (int)HttpStatusCode.InternalServerError, "An unknown error occurred during measurements update", null);
            }
            catch (Exception e)
            {
                return new OperationResultDto<MeasurementsDto[]>(false, (int)HttpStatusCode.InternalServerError,
                    $"An error occurred during measurements bulk update: {e.Message}", null);
            }
        }

        private readonly ModelContext _modelContext;
    }
}
