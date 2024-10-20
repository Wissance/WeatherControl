using System;
using Wissance.WebApiToolkit.Data.Entity;

namespace Wissance.WeatherControl.Data.Entity
{
    public class MeasurementEntity : IModelIdentifiable<Guid>
    {
        public Guid Id { get; }
        public DateTimeOffset SampleDate { get; set; }
        public decimal Value { get; set; }
    }
}