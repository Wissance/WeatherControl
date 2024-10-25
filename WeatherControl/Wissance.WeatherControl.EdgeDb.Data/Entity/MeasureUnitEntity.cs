using System;
using EdgeDB;
using Wissance.WeatherControl.Data.Model;

namespace Wissance.WeatherControl.EdgeDb.Data.Entity
{
    public class MeasureUnitEntity : IMeasureUnit
    {
        //todo(UMV): add constructor with parameters
        public MeasureUnitEntity()
        {
        }

        [EdgeDBProperty("id")]
        public Guid Id { get; set; }
        [EdgeDBProperty("Name")]
        public string Name { get; set; }
        [EdgeDBProperty("Abbreviation")]
        public string Abbreviation { get; set; }
        [EdgeDBProperty("Description")]
        public string Description { get; set; }
    }
}
