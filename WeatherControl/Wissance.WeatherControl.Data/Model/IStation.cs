using System;
using System.Collections.Generic;
using Wissance.WebApiToolkit.Data.Entity;

namespace Wissance.WeatherControl.Data.Model
{
    public interface IStation<S, MU, M> : IModelIdentifiable<Guid>
        where S : ISensor<MU, M>
        where MU : IMeasureUnit
        where M: IMeasurement
    {
        public Guid Id { get; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        
        public IList<S> Sensors { get; set; }
    }
}