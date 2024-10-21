using System;
using Wissance.WeatherControl.Data.Model;

namespace Wissance.WeatherControl.Data.Entity
{
    public class MeasureUnitEntity : IMeasureUnit
    {
        public Guid Id { get; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Abbreviation { get; set; }
    }
}