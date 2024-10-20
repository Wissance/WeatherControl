using System;
using Wissance.WebApiToolkit.Data.Entity;

namespace Wissance.WeatherControl.Data.Entity
{
    public class MeasureUnitEntity : IModelIdentifiable<Guid>
    {
        public Guid Id { get; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Abbreviation { get; set; }
    }
}