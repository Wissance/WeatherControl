using System;

namespace Wissance.WeatherControl.Dto
{
    public class SensorMinData
    {
        public SensorMinData()
        {
        }
        
        public SensorMinData(Guid id, string name, string description, string latitude, string longitude)
        {
            Id = id;
            Name = name;
            Description = description;
            Latitude = latitude;
            Longitude = longitude;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}