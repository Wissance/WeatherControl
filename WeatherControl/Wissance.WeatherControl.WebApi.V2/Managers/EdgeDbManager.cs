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
using Wissance.WeatherControl.WebApi.V2.Extensions;
using Wissance.WeatherControl.WebApi.V2.Helpers;
using Wissance.WebApiToolkit.Dto;

namespace Wissance.WeatherControl.WebApi.V2.Managers
{
    public class EdgeDbManager<TRes, TObj, TId> : IModelManager<TRes, TObj, TId>
                                                where TObj : class, IModelIdentifiable<TId>
                                                where TRes : class
                                                where TId : IComparable
    {
        public EdgeDbManager(ModelType model, EdgeDBClient edgeDbClient, Func<TObj, TRes> factory, 
            Func<TRes, bool, string, IDictionary<string, object?>> createParamsExtract)
        {
            _model = model;
            _resolver = new EqlResolver();
            _edgeDbClient = edgeDbClient;
            _factory = factory;
            _createParamsExtract = createParamsExtract;
        }
        
        public async Task<OperationResultDto<Tuple<IList<TRes>,long>>> GetAsync(int page, int size, IDictionary<string, string> parameters)
        {
            try
            {
                string countQuery = _resolver.GetQueryToCountItems(_model);
                if (countQuery == null)
                    throw new NotSupportedException($"EQL count query for model {_model} is not ready");
                long totalItems = await _edgeDbClient.QuerySingleAsync<long>(countQuery);
                // todo: move to dictionary ...
                string getQuery = _resolver.GetQueryToFetchManyItems(_model, (page - 1) * size, size, parameters);
                if (getQuery == null)
                    throw new NotSupportedException($"EQL get query for model {_model} is not ready");
                IReadOnlyCollection<TObj> items = await _edgeDbClient.QueryAsync<TObj>(getQuery);
                IList<TRes> dtoItems = items.Select(i => _factory(i)).ToList();
                return new OperationResultDto<Tuple<IList<TRes>, long>>(true, (int)HttpStatusCode.OK, String.Empty, 
                    new Tuple<IList<TRes>, long>(dtoItems, totalItems));
            }
            catch (Exception e)
            {
                // todo(UMV): log here ..
                return new OperationResultDto<Tuple<IList<TRes>, long>>(false, (int)HttpStatusCode.InternalServerError, 
                    $"An error occurred during data fetch: {e.Message}", new Tuple<IList<TRes>, long>(null, 0));;
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

        public async Task<OperationResultDto<TRes>> CreateAsync(TRes data)
        {
            try
            {
                if (_createParamsExtract == null)
                {
                    return new OperationResultDto<TRes>(false, (int)HttpStatusCode.NotImplemented,
                        "This controller is read only", null);
                }
                string query = _resolver.GetQueryToInsertItem(_model);
                if (query == null)
                    throw new NotSupportedException($"EQL query for create model {_model} item is not ready");
                IDictionary<string, object?> parameters = _createParamsExtract(data, true, null);
                await _edgeDbClient.ExecuteAsync(query, parameters);
                // ??? how to get result ?
                // https://www.edgedb.com/docs/stdlib/cfg
                // from CLI : edgedb configure set allow_user_specified_id true 
                TId id = (TId)parameters["id"]; // THIS IS A HACK TO GET CREATED OBJECT BACK
                OperationResultDto<TRes> result = await GetByIdAsync(id);
                return new OperationResultDto<TRes>(true, (int)HttpStatusCode.Created, String.Empty, result.Data);
            }
            catch (Exception e)
            {
                return new OperationResultDto<TRes>(false, (int)HttpStatusCode.InternalServerError,
                    $"An error occurred during new item creation, error: {e.Message}", null);
            }
        }

        public async Task<OperationResultDto<TRes[]>> BulkCreateAsync(TRes[] data)
        {
            try
            {
                if (_createParamsExtract == null)
                {
                    return new OperationResultDto<TRes[]>(false, (int)HttpStatusCode.NotImplemented,
                        "This controller is not support bulk create operation", null);
                }
                
                string createQuery = _resolver.GetQueryToInsertMultipleItems(_model, data.Length);
                if (createQuery == null)
                    throw new NotSupportedException($"EQL query for bulk create model {_model} item is not ready");
                TId[] createdObjects = new TId[data.Length];
                IDictionary<string, object?>[] parameters = new IDictionary<string, object?>[data.Length];
                //Dictionary<string, object?> parameters = new Dictionary<string, object?>();
                for (int i = 0; i < data.Length; i++)
                {
                    IDictionary<string, object?> itemParams = _createParamsExtract(data[i], true, i.ToString());
                    parameters[i]=itemParams;
                    string itemId = $"id{i}";
                    createdObjects[0] = (TId)itemParams[itemId];
                }
                
                IDictionary<string, object?> bulkParams = new Dictionary<string, object?>()
                {
                    {"data", parameters}
                };
                
                await _edgeDbClient.ExecuteAsync(createQuery, bulkParams);
                string getQuery = _resolver.GetQueryToGetManyItemsWithFilterById(_model);
                if (getQuery == null)
                {
                    throw new Exception("Get query to get many items by id in list is not defined");
                }
                IReadOnlyCollection<TObj> items = await _edgeDbClient.QueryAsync<TObj>(getQuery);
                TRes[] dtoItems = items.Select(i => _factory(i)).ToArray();
                return new OperationResultDto<TRes[]>(true, (int)HttpStatusCode.Created, String.Empty, dtoItems);
            }
            catch (Exception e)
            { 
                return new OperationResultDto<TRes[]>(false, (int)HttpStatusCode.InternalServerError,
                    $"An error occurred during new item creation, error: {e.Message}", null);
            }
        }

        // todo(UMV): Create and Update looking similar: create internal function that will combine it work
        public async Task<OperationResultDto<TRes>> UpdateAsync(TId id, TRes data)
        {
            try
            {
                if (_createParamsExtract == null)
                {
                    return new OperationResultDto<TRes>(false, (int)HttpStatusCode.NotImplemented,
                        "This controller is read only", null);
                }
                string query = _resolver.GetQueryToUpdateItem(_model);
                if (query == null)
                    throw new NotSupportedException($"EQL query for update model {_model} item is not ready");
                IDictionary<string, object?> parameters = _createParamsExtract(data, false, null);
                await _edgeDbClient.ExecuteAsync(query, parameters);
                OperationResultDto<TRes> result = await GetByIdAsync(id);
                return result;
            }
            catch (Exception e)
            {
                return new OperationResultDto<TRes>(false, (int)HttpStatusCode.InternalServerError,
                    $"An error occurred during update item with id: {id} , error: {e.Message}", null);
            }
        }

        public async Task<OperationResultDto<TRes[]>> BulkUpdateAsync(TRes[] data)
        {
            try
            {
                if (_createParamsExtract == null)
                {
                    return new OperationResultDto<TRes[]>(false, (int)HttpStatusCode.NotImplemented,
                        "This controller is not support bulk update operation", null);
                }
                string updateQuery = _resolver.GetQueryToUpdateMultipleItems(_model, data.Length);
                if (updateQuery == null)
                    throw new NotSupportedException($"EQL query for bulk update model {_model} item is not ready");
                TId[] createdObjects = new TId[data.Length];
                Dictionary<string, object?> parameters = new Dictionary<string, object?>();
                for (int i = 0; i < data.Length; i++)
                {
                    IDictionary<string, object?> itemParams = _createParamsExtract(data[i], false, i.ToString());
                    parameters.AddRange(itemParams);
                    string itemId = $"id{i}";
                    createdObjects[0] = (TId)itemParams[itemId];
                }
                await _edgeDbClient.ExecuteAsync(updateQuery, parameters);
                string getQuery = _resolver.GetQueryToGetManyItemsWithFilterById(_model);
                if (getQuery == null)
                {
                    throw new Exception("Get query to get many items by id in list is not defined");
                }
                IReadOnlyCollection<TObj> items = await _edgeDbClient.QueryAsync<TObj>(getQuery);
                TRes[] dtoItems = items.Select(i => _factory(i)).ToArray();
                return new OperationResultDto<TRes[]>(true, (int)HttpStatusCode.OK, String.Empty, dtoItems);
            }
            catch (Exception e)
            {
                return new OperationResultDto<TRes[]>(false, (int)HttpStatusCode.InternalServerError,
                    $"An error occurred during update multiple items, error: {e.Message}", null);
            }
        }

        public async Task<OperationResultDto<bool>> DeleteAsync(TId id)
        {
            try
            {
                string query = _resolver.GetQueryToDeleteItem(_model);
                if (query == null)
                    throw new NotSupportedException($"EQL query for delete model {_model} item is not ready");
                await _edgeDbClient.ExecuteAsync(query, new Dictionary<string, object?>()
                {
                    {"id", id}
                });
                return new OperationResultDto<bool>(true, (int)HttpStatusCode.NoContent, string.Empty, true);
            }
            catch (Exception e)
            {
                return new OperationResultDto<bool>(false, (int)HttpStatusCode.InternalServerError,
                    $"An error occurred during delete item with id: {id} , error: {e.Message}", false);
            }
        }

        public async Task<OperationResultDto<bool>> BulkDeleteAsync(TId[] objectsIds)
        {
            try
            {
                string query = _resolver.GetQueryToDeleteMultipleItems(_model);
                if (query == null)
                    throw new NotSupportedException($"EQL query for bulk delete model {_model} items is not ready");
                await _edgeDbClient.ExecuteAsync(query, new Dictionary<string, object?>()
                {
                    {"idList", objectsIds}
                });
                return new OperationResultDto<bool>(true, (int)HttpStatusCode.NoContent, string.Empty, true);
            }
            catch (Exception e)
            {
                return new OperationResultDto<bool>(false, (int)HttpStatusCode.InternalServerError,
                    $"An error occurred during delete multiple items, error: {e.Message}", false);
            }
        }

        private readonly ModelType _model;
        private readonly EqlResolver _resolver;
        private readonly EdgeDBClient _edgeDbClient;
        private readonly Func<TObj, TRes> _factory;
        private readonly Func<TRes, bool, string, IDictionary<string, object?>> _createParamsExtract;
    }
}