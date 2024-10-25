using System;
using System.Collections.Generic;
using EdgeDB;
using Wissance.WeatherControl.Data.Model;

namespace Wissance.WeatherControl.EdgeDb.Data.Entity
{
    public class StationEntity : IStation<SensorEntity, MeasureUnitEntity, MeasurementEntity>
    {
        //todo(UMV): add constructor with parameters
        public StationEntity()
        {
            Sensors = new List<SensorEntity>();
        }

        [EdgeDBProperty("id")]
        public Guid Id { get; set; }

        [EdgeDBProperty("Name")]
        public string Name { get; set; }
        
        [EdgeDBProperty("Description")]
        public string Description { get; set; }

        [EdgeDBProperty("Longitude")]
        public string Longitude { get; set; }
        [EdgeDBProperty("Latitude")]
        public string Latitude { get; set; }
        [EdgeDBProperty("Sensors")]
        public IList<SensorEntity> Sensors { get; set; }
    }
}