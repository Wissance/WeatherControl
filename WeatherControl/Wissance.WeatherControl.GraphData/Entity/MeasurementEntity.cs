using System;
using EdgeDB;
using Wissance.WebApiToolkit.Data.Entity;

namespace Wissance.WeatherControl.GraphData.Entity
{
    public class MeasurementEntity : IModelIdentifiable<Guid>
    {
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