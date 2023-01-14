namespace Wissance.WeatherControl.Dto.V2
{
    public class MeasurementDto
    {
        public Guid? Id { get; set; }
        public Guid MeasureUnitId { get; set; }
        public Guid SensorId { get; set; }
        public decimal Value { get; set; }
    }
}