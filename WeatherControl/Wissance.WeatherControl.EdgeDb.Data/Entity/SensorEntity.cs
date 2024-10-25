using System;
using System.Collections.Generic;
using EdgeDB;
using Wissance.WeatherControl.Data.Model;

namespace Wissance.WeatherControl.EdgeDb.Data.Entity
{
    public class SensorEntity : ISensor<MeasureUnitEntity,MeasurementEntity>
    {
        //todo(UMV): add constructor with parameters

        public SensorEntity()
        {
            Measurements = new List<MeasurementEntity>();
        }

        [EdgeDBProperty("id")]
        public Guid Id { get; set; }
        
        [EdgeDBProperty("Name")]
        public string Name { get; set; }
        
        [EdgeDBProperty("Description")]
        public string Description { get; set; }

        [EdgeDBProperty("Latitude")]
        public string Latitude { get; set; }
        
        [EdgeDBProperty("Longitude")]
        public string Longitude { get; set; }

        [EdgeDBProperty("MeasureUnit")]
        public MeasureUnitEntity MeasureUnit { get; set; }

        [EdgeDBProperty("Measurements")]
        public IList<MeasurementEntity> Measurements { get; set; }
    }
}