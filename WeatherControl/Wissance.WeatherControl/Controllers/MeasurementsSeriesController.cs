using Wissance.WeatherControl.Data.Entity;
using Wissance.WeatherControl.Dto;
using Wissance.WeatherControl.WebApi.Managers;
using Wissance.WebApiToolkit.Controllers;

namespace Wissance.WeatherControl.WebApi.Controllers
{
    public class MeasurementsSeriesController: BasicBulkCrudController<MeasurementsDto, MeasurementsEntity, int>
    {
        public MeasurementsSeriesController(MeasurementsManager manager)
        {
            Manager = manager;  // this is for basic operations
            _manager = manager; // this for extended operations
        }

        private MeasurementsManager _manager;
    }
}