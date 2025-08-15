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
using Wissance.WeatherControl.WebApi.Helpers.Filtering;
using Wissance.WebApiToolkit.Dto;
using Wissance.WebApiToolkit.Ef.Managers;

namespace Wissance.WeatherControl.WebApi.Managers
{
    public class SensorManager: EfModelManager<SensorDto, SensorEntity,  Guid>
    {
        public SensorManager(ModelContext modelContext, ILoggerFactory loggerFactory) 
            : base(modelContext, SensorFilter.Filter, SensorFactory.Create, SensorFactory.Create, 
                null, loggerFactory)
        {
            _modelContext = modelContext;
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