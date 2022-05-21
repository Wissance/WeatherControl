using System;
using System.Collections.Generic;
using System.Linq;
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
    public class StationManager : ModelManager<StationEntity, StationDto, int>
    {
        public StationManager(ModelContext modelContext, ILoggerFactory loggerFactory) : base(loggerFactory)
        {
            _modelContext = modelContext;
        }

        public override async Task<OperationResultDto<IList<StationDto>>> GetAsync(int page, int size)
        {
            return await GetAsync<int>(_modelContext.Stations, page, size, null, null, StationFactory.Create);
        }

        public override async Task<OperationResultDto<StationDto>> GetByIdAsync(int id)
        {
            return await GetAsync(_modelContext.Stations, id, StationFactory.Create);
        }

        public override async Task<OperationResultDto<StationDto>> CreateAsync(StationDto data)
        {
            try
            {
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public override async Task<OperationResultDto<StationDto>> UpdateAsync(int id, StationDto data)
        {
            return base.UpdateAsync(id, data);
        }

        public override async Task<OperationResultDto<bool>> DeleteAsync(int id)
        {
            return await DeleteAsync(_modelContext, _modelContext.Stations, id);
        }

        private readonly ModelContext _modelContext;
    }
}
