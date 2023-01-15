using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using Wissance.WebApiToolkit.Data.Entity;
using Wissance.WebApiToolkit.Managers;
using Wissance.WeatherControl.Dto.V2;
using Wissance.WeatherControl.GraphData.Entity;
using EdgeDB;
using System.Threading.Tasks;
using Wissance.WeatherControl.GraphData;
using Wissance.WeatherControl.WebApi.V2.Helpers;
using Wissance.WebApiToolkit.Dto;

namespace Wissance.WeatherControl.WebApi.V2.Managers
{
    public class EdgeDbManager<TRes, TObj, TId> : IModelManager<TRes, TObj, TId>
                                                where TObj : class, IModelIdentifiable<TId>
                                                where TRes : class
                                                where TId : IComparable
    {
        public EdgeDbManager(ModelType model, EdgeDBClient edgeDbClient, Func<TObj, TRes> factory)
        {
            _model = model;
            _resolver = new EqlResolver();
            _edgeDbClient = edgeDbClient;
            _factory = factory;
        }

        public async Task<OperationResultDto<TRes>> CreateAsync(TRes data)
        {
            //_edgeDbClient.
            throw new NotImplementedException();
        }

        public async Task<OperationResultDto<bool>> DeleteAsync(TId id)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResultDto<IList<TRes>>> GetAsync(int page, int size)
        {
            try
            {
                // todo: mode to query ...
                string query = _resolver.GetQueryToFetchManyItems(_model, (page - 1) * size, size);
                if (query == null)
                    throw new NotSupportedException($"EQL queries for model {_model} are not ready");
                IReadOnlyCollection<TObj> items = await _edgeDbClient.QueryAsync<TObj>(query);
                // todo: limit
                IList<TRes> dtoItems = items.Select(i => _factory(i)).ToList();
                return new OperationResultDto<IList<TRes>>(true, (int)HttpStatusCode.OK, String.Empty, dtoItems);
            }
            catch (Exception e)
            {
                // todo(UMV): log here ..
                return new OperationResultDto<IList<TRes>>(false, (int)HttpStatusCode.InternalServerError, 
                    $"An error occurred during data fetch: {e.Message}", null);;
            }
        }

        public async Task<OperationResultDto<TRes>> GetByIdAsync(TId id)
        {
            try
            { 
                string query = _resolver.GetQueryToGetOneItem(_model);
                if (query == null)
                    throw new NotSupportedException($"EQL queries for model {_model} are not ready");
                TObj item = await _edgeDbClient.QuerySingleAsync<TObj>(query, new Dictionary<string, object?>()
                {
                    {"id", id}
                });
                if (item != null)
                {
                    return new OperationResultDto<TRes>(true, (int)HttpStatusCode.OK, String.Empty, _factory(item));
                }
                return new OperationResultDto<TRes>(false, (int)HttpStatusCode.NotFound, $"Object with id: {id} was not found", null);
            }
            catch (Exception e)
            {
                return new OperationResultDto<TRes>(false, (int)HttpStatusCode.InternalServerError, 
                    $"An error occurred during getting object of model type {_model} by id: {id}, error: {e.Message}", null);
            }
            
        }

        public async Task<OperationResultDto<TRes>> UpdateAsync(TId id, TRes data)
        {
            throw new NotImplementedException();
        }

        private readonly ModelType _model;
        private readonly EqlResolver _resolver;
        private readonly EdgeDBClient _edgeDbClient;
        private readonly Func<TObj, TRes> _factory;

    }
}