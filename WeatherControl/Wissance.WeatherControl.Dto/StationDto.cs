using System;
using System.Collections.Generic;
using System.Text;

namespace Wissance.WeatherControl.Dto
{
    public class StationDto
    {
        public StationDto()
        {
            Sensors = new List<SensorMinData>();
        }

        public StationDto(Guid id, string name, string description, string longitude, string latitude,
            IList<SensorMinData> sensors)
        {
            Id = id;
            Name = name;
            Description = description;
            Longitude = longitude;
            Latitude = latitude;
            Sensors = sensors;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public IList<SensorMinData> Sensors { get; set; }
    }
}
