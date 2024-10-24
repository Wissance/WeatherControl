using System;

namespace Wissance.WeatherControl.Dto
{
    public class SensorMinDataDto
    {
        public SensorMinDataDto()
        {
        }
        
        public SensorMinDataDto(Guid id, string name, string description, string latitude, string longitude, 
            Guid stationId, Guid measureUnitId)
        {
            Id = id;
            Name = name;
            Description = description;
            Latitude = latitude;
            Longitude = longitude;
            StationId = stationId;
            MeasureUnitId = measureUnitId;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public Guid StationId { get; set; }
        public Guid MeasureUnitId { get; set; }
    }
}