using System;
using EdgeDB;
using Wissance.WeatherControl.Data.Model;

namespace Wissance.WeatherControl.EdgeDb.Data.Entity
{
    public class MeasurementEntity : IMeasurement
    {
        //todo(UMV): add constructor with parameters
        public MeasurementEntity()
        {
            
        }

        [EdgeDBProperty("id")]
        public Guid Id { get; set; }
        [EdgeDBProperty("SampleDate")]
        public DateTimeOffset SampleDate { get; set; }
        [EdgeDBProperty("Unit")]
        public MeasureUnitEntity Unit { get; set; }
        [EdgeDBProperty("Value")]
        public decimal Value { get; set; }
        [EdgeDBProperty("Sensor")]
        public SensorEntity Sensor { get; set; }
    }
}