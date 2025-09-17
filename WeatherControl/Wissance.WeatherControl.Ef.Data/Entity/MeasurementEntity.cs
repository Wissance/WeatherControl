using System;
using Wissance.WeatherControl.Data.Model;
using Wissance.WebApiToolkit.Data.Entity;

namespace Wissance.WeatherControl.Data.Entity
{
    public class MeasurementEntity : IModelIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public DateTimeOffset SampleDate { get; set; }
        public decimal Value { get; set; }
        public Guid SensorId { get; set; }
        
        public virtual SensorEntity Sensor { get; set; }
    }
}