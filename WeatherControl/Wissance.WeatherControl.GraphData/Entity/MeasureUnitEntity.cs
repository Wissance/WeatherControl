using System;
using EdgeDB;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wissance.WebApiToolkit.Data.Entity;

namespace Wissance.WeatherControl.GraphData.Entity
{
    public class MeasureUnitEntity : IModelIdentifiable<Guid>
    {
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
