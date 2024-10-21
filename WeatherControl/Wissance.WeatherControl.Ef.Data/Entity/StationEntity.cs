using System;
using System.Collections.Generic;
using Wissance.WeatherControl.Data.Model;

namespace Wissance.WeatherControl.Data.Entity
{
    public class StationEntity : IStation<SensorEntity, MeasureUnitEntity, MeasurementEntity>
    {
        public Guid Id { get; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        
        public virtual IList<SensorEntity> Sensors { get; set; }
    }
}