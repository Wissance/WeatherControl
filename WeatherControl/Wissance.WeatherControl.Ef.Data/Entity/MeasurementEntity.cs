using System;
using Wissance.WeatherControl.Data.Model;

namespace Wissance.WeatherControl.Data.Entity
{
    public class MeasurementEntity : IMeasurement
    {
        public Guid Id { get; }
        public DateTimeOffset SampleDate { get; set; }
        public decimal Value { get; set; }
        public Guid SensorId { get; set; }
        
        public SensorEntity Sensor { get; set; }
    }
}