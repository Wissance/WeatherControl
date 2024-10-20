using System;
using System.Collections.Generic;
using Wissance.WebApiToolkit.Data.Entity;

namespace Wissance.WeatherControl.Data.Entity
{
    public class StationEntity : IModelIdentifiable<Guid>
    {
        public Guid Id { get; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        
        public virtual IList<SensorEntity> Sensors { get; set; }
    }
}