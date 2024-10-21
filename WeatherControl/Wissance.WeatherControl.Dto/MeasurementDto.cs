using System;

namespace Wissance.WeatherControl.Dto
{
    public class MeasurementDto
    {
        public MeasurementDto()
        {
        }

        public MeasurementDto(Guid id, DateTimeOffset sampleDate, decimal value, Guid? sensorId)
        {
            Id = id;
            SampleDate = sampleDate;
            Value = value;
            SensorId = sensorId;
        }

        public Guid Id { get; set; }
        public DateTimeOffset SampleDate { get; set; }
        public decimal Value { get; set; }
        public Guid? SensorId { get; set; }
    }
}