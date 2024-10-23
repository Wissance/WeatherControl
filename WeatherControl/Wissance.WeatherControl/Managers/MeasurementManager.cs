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
    public class MeasurementManager : EfModelManager<MeasurementEntity, MeasurementDto, Guid>
    {
        public MeasurementManager(ModelContext modelContext, ILoggerFactory loggerFactory) 
            : base(modelContext, MeasurementsFilter.Filter, MeasurementFactory.Create, loggerFactory)
        {
            _modelContext = modelContext;
        }

        public override async Task<OperationResultDto<MeasurementDto>> CreateAsync(MeasurementDto data)
        {
            try
            {
                MeasurementEntity entity = MeasurementFactory.Create(data);
                await _modelContext.Measurements.AddAsync(entity);
                int result = await _modelContext.SaveChangesAsync();
                if (result >= 0)
                {
                    return new OperationResultDto<MeasurementDto>(true, (int)HttpStatusCode.Created, null, MeasurementFactory.Create(entity));
                }
                return new OperationResultDto<MeasurementDto>(false, (int)HttpStatusCode.InternalServerError, 
                    "An unknown error occurred during \"Measurement\" creation", null);
            }
            catch (Exception e)
            {
                return new OperationResultDto<MeasurementDto>(false, (int)HttpStatusCode.InternalServerError,
                    $"An error occurred during \"Measurement\" create: {e.Message}", null);
            }
            
        }

        public override async Task<OperationResultDto<MeasurementDto[]>> BulkCreateAsync(MeasurementDto[] data)
        {
            try
            {
                IList<MeasurementEntity> measurements = data.Select(d => MeasurementFactory.Create(d)).ToList();
                await _modelContext.Measurements.AddRangeAsync(measurements);
                int result = await _modelContext.SaveChangesAsync();
                if (result >= 0)
                {
                    return new OperationResultDto<MeasurementDto[]>(true, (int)HttpStatusCode.Created, null,
                        measurements.Select(m => MeasurementFactory.Create(m)).ToArray());
                }
                return new OperationResultDto<MeasurementDto[]>(false, (int)HttpStatusCode.InternalServerError, "An unknown error occurred during measurements creation", null);
            }
            catch (Exception e)
            {
                return new OperationResultDto<MeasurementDto[]>(false, (int)HttpStatusCode.InternalServerError,
                    $"An error occurred during \"Measurement\" bulk create: {e.Message}", null);
            }
        }

        public override async Task<OperationResultDto<MeasurementDto>> UpdateAsync(Guid id, MeasurementDto data)
        {
            try
            {
                MeasurementEntity entity = MeasurementFactory.Create(data);
                MeasurementEntity existingEntity = await _modelContext.Measurements.FirstOrDefaultAsync(m => m.Id == id);
                if (existingEntity == null)
                {
                    return new OperationResultDto<MeasurementDto>(false, (int)HttpStatusCode.NotFound, $"Measurements with id: {id} does not exists", null);
                }
                existingEntity.Value = entity.Value;
                existingEntity.SampleDate = entity.SampleDate;
                if (existingEntity.SensorId != entity.SensorId)
                {
                    existingEntity.SensorId = entity.SensorId;
                }

                int result = await _modelContext.SaveChangesAsync();
                if (result >= 0)
                {
                    return new OperationResultDto<MeasurementDto>(true, (int)HttpStatusCode.OK, null, MeasurementFactory.Create(existingEntity));
                }
                return new OperationResultDto<MeasurementDto>(false, (int)HttpStatusCode.InternalServerError, $"An unknown error occurred during measurements update", null);
            }
            catch (Exception e)
            {
                return new OperationResultDto<MeasurementDto>(false, (int)HttpStatusCode.InternalServerError,
                    $"An error occurred during \"Measurement\" update: {e.Message}", null);
            }
        }
        
        public override async Task<OperationResultDto<MeasurementDto[]>> BulkUpdateAsync(MeasurementDto[] data)
        {
            try
            {
                IList<MeasurementEntity> measurementsToUpdate = await _modelContext.Measurements.Where(m => data.Any(di => di.Id == m.Id))
                                                                                          .ToListAsync();
                foreach (MeasurementEntity measurement in measurementsToUpdate)
                {
                    MeasurementDto dto = data.First(m => m.Id == measurement.Id);
                    measurement.Value = dto.Value;
                    measurement.SampleDate = dto.SampleDate;
                    if (dto.SensorId.HasValue && measurement.SensorId != dto.SensorId)
                    {
                        measurement.SensorId = dto.SensorId.Value;
                    }
                }
                int result = await _modelContext.SaveChangesAsync();
                if (result >= 0)
                {
                    return new OperationResultDto<MeasurementDto[]>(true, (int)HttpStatusCode.OK, null,
                        measurementsToUpdate.Select(m => MeasurementFactory.Create(m)).ToArray());
                }
                return new OperationResultDto<MeasurementDto[]>(false, (int)HttpStatusCode.InternalServerError, "An unknown error occurred during measurements update", null);
            }
            catch (Exception e)
            {
                return new OperationResultDto<MeasurementDto[]>(false, (int)HttpStatusCode.InternalServerError,
                    $"An error occurred during \"Measurement\" bulk update: {e.Message}", null);
            }
        }

        private readonly ModelContext _modelContext;
    }
}
