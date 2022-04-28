using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wissance.WeatherControl.Data.Entity;
using Wissance.WeatherControl.Dto;
using Wissance.WebApiToolkit.Dto;
using Wissance.WebApiToolkit.Managers;

namespace Wissance.WeatherControl.WebApi.Managers
{
    public class StationManager : IModelManager<StationDto, StationEntity, int>
    {
        public Task<OperationResultDto<StationDto>> CreateAsync(StationDto data)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResultDto<StationDto>> UpdateAsync(int id, StationDto data)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResultDto<bool>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResultDto<IList<StationDto>>> GetAsync(int page, int size)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResultDto<StationDto>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
