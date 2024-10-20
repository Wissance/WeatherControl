using System;

namespace Wissance.WeatherControl.Dto
{
    public class MeasurementDto
    {
        public Guid Id { get; }
        public DateTimeOffset SampleDate { get; set; }
        public decimal Value { get; set; }
    }
}