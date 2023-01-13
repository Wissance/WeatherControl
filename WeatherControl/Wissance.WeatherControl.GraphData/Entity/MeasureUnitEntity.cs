using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wissance.WebApiToolkit.Data.Entity;

namespace Wissance.WeatherControl.GraphData.Entity
{
    public class MeasureUnitEntity : IModelIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public string Description { get; set; }
    }
}
