using System;
using EdgeDB;
using Wissance.WeatherControl.Dto.V2;
using Wissance.WeatherControl.GraphData;
using Wissance.WeatherControl.GraphData.Entity;
using Wissance.WeatherControl.WebApi.V2.Factories;
using Wissance.WeatherControl.WebApi.V2.Managers;
using Wissance.WebApiToolkit.Controllers;

namespace Wissance.WeatherControl.WebApi.V2.Controllers
{
    /// <summary>
    ///    This controller has ability to recieve all possible query params, but EdgeDbManager could work with 3 of them:
    ///    * sensor - id of sensor
    ///    * from - measurements that were captured from specific datetime
    ///    * to  - measurements that were captured previously to specific date
    ///  I.e.: parameters usage : http://localhost:8058/api/Measurement?sensor=30ac8366-ea4b-11ed-ab17-e38db76a95f0&to=2023-05-01T12:01:00%2B05:00&from=2023-05-01T11:31:00%2B05:00
    ///  Note: + MUST BE ENCODED AS %2B
    /// </summary>
    public class MeasurementController : BasicCrudController<MeasurementDto, MeasurementEntity, Guid>
    {
        public MeasurementController(EdgeDBClient edgeDbClient)
        {
            Manager = new EdgeDbManager<MeasurementDto, MeasurementEntity, Guid>(ModelType.Measurement, edgeDbClient,
                MeasurementFactory.Create, MeasurementFactory.Create);
        }
    }
}