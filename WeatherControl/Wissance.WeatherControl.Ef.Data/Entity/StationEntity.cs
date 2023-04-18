using System.Collections.Generic;
using Wissance.WebApiToolkit.Data.Entity;

namespace Wissance.WeatherControl.Data.Entity
{
    /// <summary>
    ///    Meteorological station
    /// </summary>
    public class StationEntity : IModelIdentifiable<int>
    {
        public StationEntity()
        {
            Measurements = new List<MeasurementsEntity>();
        }

        public StationEntity(string name, string description, string longitude, string latitude, IList<MeasurementsEntity> measurements)
        {
            Name = name;
            Description = description;
            Longitude = longitude;
            Latitude = latitude;
            Measurements = measurements;
        }

        public int Id { get; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public virtual IList<MeasurementsEntity> Measurements { get; set; }
    }
}
