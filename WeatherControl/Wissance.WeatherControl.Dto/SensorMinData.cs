using System;

namespace Wissance.WeatherControl.Dto
{
    public class SensorMinData
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}