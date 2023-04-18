using System;
using Wissance.WebApiToolkit.Data.Entity;

namespace Wissance.WeatherControl.Data.Entity
{
    public class MeasurementsEntity : IModelIdentifiable<int>
    {
        public MeasurementsEntity()
        {
        }

        public MeasurementsEntity(DateTime timestamp, double? temperature, double? pressure, double? humidity, double? windSpeed, int stationId)
        {
            Timestamp = timestamp;
            Temperature = temperature;
            Pressure = pressure;
            Humidity = humidity;
            WindSpeed = windSpeed;
            StationId = stationId;
        }

        public int Id { get; }
        public DateTime Timestamp { get; set; }
        public double? Temperature { get; set; }
        public double? Pressure { get; set; }
        public double? Humidity { get; set; }
        public double? WindSpeed { get; set; }
        public int StationId { get; set; }

        public virtual StationEntity Station { get; set; }
    }
}
