using Wissance.WebApiToolkit.Core.Controllers;
using Wissance.WebApiToolkit.Core.Data;
using Wissance.WebApiToolkit.Core.Managers;

namespace Wissance.WeatherControl.Common.Automation
{
    public class GenericController <TRes, TData, TId, TFilter> : BasicCrudController<TRes, TData, TId, TFilter>
        where TRes : class
        where TFilter: class, IReadFilterable
    {
        public GenericController(IModelManager<TRes, TData, TId> manager)
        {
            Manager = manager;
        }
    }
}