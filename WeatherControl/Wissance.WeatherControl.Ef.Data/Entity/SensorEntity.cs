using System;
using System.Collections.Generic;
using Wissance.WeatherControl.Data.Model;
using Wissance.WebApiToolkit.Data.Entity;

namespace Wissance.WeatherControl.Data.Entity
{
    public class SensorEntity : IModelIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public Guid StationId { get; set; }
        public Guid MeasureUnitId { get; set; }
        
        public virtual StationEntity Station { get; set; }
        public virtual MeasureUnitEntity MeasureUnit { get; set; }
        public virtual IList<MeasurementEntity> Measurements { get; set; }
    }
}