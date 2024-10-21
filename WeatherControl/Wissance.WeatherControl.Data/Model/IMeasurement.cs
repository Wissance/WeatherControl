using System;
using Wissance.WebApiToolkit.Data.Entity;

namespace Wissance.WeatherControl.Data.Model
{
    public interface IMeasurement : IModelIdentifiable<Guid>
    {
        public Guid Id { get; }
        public DateTimeOffset SampleDate { get; set; }
        public decimal Value { get; set; }
    }
}