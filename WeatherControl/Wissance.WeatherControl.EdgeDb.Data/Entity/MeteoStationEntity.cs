using System;
using System.Collections.Generic;
using EdgeDB;
using Wissance.WebApiToolkit.Data.Entity;

namespace Wissance.WeatherControl.GraphData.Entity
{
    public class MeteoStationEntity : IModelIdentifiable<Guid>
    {
        //todo(UMV): add constructor with parameters
        public MeteoStationEntity()
        {
            Sensors = new List<SensorEntity>();
        }

        [EdgeDBProperty("id")]
        public Guid Id { get; set; }
        [EdgeDBProperty("Longitude")]
        public string Longitude { get; set; }
        [EdgeDBProperty("Latitude")]
        public string Latitude { get; set; }
        [EdgeDBProperty("Sensors")]
        public IList<SensorEntity> Sensors { get; set; }
    }
}