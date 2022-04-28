using System;
using System.Collections.Generic;
using System.Text;

namespace Wissance.WeatherControl.Dto
{
    public class MeasurementsDto
    {
        public MeasurementsDto()
        {

        }

        public MeasurementsDto(int id, DateTime timestamp, double? temperature, double? pressure, double? humidity, double? windSpeed, int stationId)
        {
            Id = id;
            Timestamp = timestamp;
            Temperature = temperature;
            Pressure = pressure;
            Humidity = humidity;
            WindSpeed = windSpeed;
            StationId = stationId;
        }

        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public double? Temperature { get; set; }
        public double? Pressure { get; set; }
        public double? Humidity { get; set; }
        public double? WindSpeed { get; set; }
        public int StationId { get; set; }
    }
}
