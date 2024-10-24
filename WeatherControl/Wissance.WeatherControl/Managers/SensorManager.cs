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
using Wissance.WebApiToolkit.Managers;

namespace Wissance.WeatherControl.WebApi.Managers
{
    public class SensorManager: EfModelManager<SensorEntity, SensorDto, Guid>
    {
        public SensorManager(ModelContext modelContext, ILoggerFactory loggerFactory) 
            : base(modelContext, null, SensorFactory.Create, loggerFactory)
        {
            _modelContext = modelContext;
        }

        public override async Task<OperationResultDto<SensorDto>> CreateAsync(SensorDto data)
        {
            try
            {
                SensorEntity entity = SensorFactory.Create(data);
                await _modelContext.Sensors.AddAsync(entity);
                
                int result = await _modelContext.SaveChangesAsync();
                if (result >= 0)
                {
                    return new OperationResultDto<SensorDto>(true, (int)HttpStatusCode.Created, null, SensorFactory.Create(entity));
                }
                return new OperationResultDto<SensorDto>(false, (int)HttpStatusCode.InternalServerError, "An unknown error occurred during \"Sensor\" creation", null);
            }
            catch (Exception e)
            {
                return new OperationResultDto<SensorDto>(false, (int) HttpStatusCode.InternalServerError,
                    $"An error occurred during \"Sensor\" create: {e.Message}", null);
            }
        }

        public override async Task<OperationResultDto<SensorDto>> UpdateAsync(Guid id, SensorDto data)
        {
            try
            {
                SensorEntity existingSensor = await _modelContext.Sensors.FirstOrDefaultAsync(s => s.Id == id);
                if (existingSensor == null)
                {
                    return new OperationResultDto<SensorDto>(false, (int) HttpStatusCode.NotFound,
                        $"An error occurred during \"Sensor\" update, an object with id:\":{id}\" does n't exist", null);
                }

                existingSensor.Name = data.Name;
                existingSensor.Description = data.Description;
                existingSensor.Latitude = data.Latitude;
                existingSensor.Longitude = data.Longitude;
                existingSensor.StationId = data.StationId;
                existingSensor.MeasureUnitId = data.MeasureUnitId;
                
                int result = await _modelContext.SaveChangesAsync();
                if (result >= 0)
                {
                    return new OperationResultDto<SensorDto>(true, (int)HttpStatusCode.OK, null, SensorFactory.Create(existingSensor));
                }
                return new OperationResultDto<SensorDto>(false, (int)HttpStatusCode.InternalServerError, "An unknown error occurred during \"Sensor\" update", null);
            }
            catch (Exception e)
            {
                return new OperationResultDto<SensorDto>(false, (int) HttpStatusCode.InternalServerError,
                    $"An error occurred during \"Sensor\" update: {e.Message}", null);
            }
        }

        private readonly ModelContext _modelContext;
    }
}