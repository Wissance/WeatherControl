using System;
using Wissance.WebApiToolkit.Data.Entity;
using Wissance.WebApiToolkit.Managers;
using Wissance.WeatherControl.Dto.V2;
using Wissance.WeatherControl.GraphData.Entity;
using EdgeDB;
using System.Threading.Tasks;
using Wissance.WebApiToolkit.Dto;

namespace Wissance.WeatherControl.WebApi.V2.Managers
{
    public abstract class EdgeDbManager<TRes, TObj, TId> : IModelManager<TRes, TObj, TId>
                                                where TObj : class, IModelIdentifiable<TId>
                                                where TRes : class
                                                where TId : IComparable
    {
        public async Task<OperationResultDto<TRes>> CreateAsync(TRes data)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResultDto<bool>> DeleteAsync(TId id)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResultDto<System.Collections.Generic.IList<TRes>>> GetAsync(int page, int size)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResultDto<TRes>> GetByIdAsync(TId id)
        {
            TObj item = await _edgeDbClient.QuerySingleAsync<TObj>("");
            return new OperationResultDto<TRes>();
        }

        public async Task<OperationResultDto<TRes>> UpdateAsync(TId id, TRes data)
        {
            throw new NotImplementedException();
        }

        private EdgeDBClient _edgeDbClient;
    }
}