using System;
using System.Collections.Generic;
using System.Text;

namespace Wissance.WeatherControl.Dto
{
    public class StationDto
    {
        public StationDto()
        {
        }

        public StationDto(int id, string name, string description, string longitude, string latitude)
        {
            Id = id;
            Name = name;
            Description = description;
            Longitude = longitude;
            Latitude = latitude;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
    }
}
