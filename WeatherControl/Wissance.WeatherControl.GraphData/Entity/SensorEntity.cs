using System;
using System.Collections.Generic;
using EdgeDB;
using Wissance.WebApiToolkit.Data.Entity;

namespace Wissance.WeatherControl.GraphData.Entity
{
    public class SensorEntity : IModelIdentifiable<Guid>
    {
        [EdgeDBProperty("id")]
        public Guid Id { get; set; }
        [EdgeDBProperty("Name")]
        public string Name { get; set; }
        [EdgeDBProperty("Latitude")]
        public string Latitude { get; set; }
        [EdgeDBProperty("Longitude")]
        public string Longitude { get; set; }
        [EdgeDBProperty("Measurements")]
        public IList<MeasurementEntity> Measurements { get; set; }
    }
}