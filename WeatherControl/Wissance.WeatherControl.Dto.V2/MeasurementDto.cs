namespace Wissance.WeatherControl.Dto.V2
{
    public class MeasurementDto
    {
        //todo(UMV): add constructor with parameters

        public MeasurementDto()
        {
        }

        public Guid? Id { get; set; }
        public DateTimeOffset SampleDate { get; set; }
        public Guid MeasureUnitId { get; set; }
        public Guid SensorId { get; set; }
        public decimal Value { get; set; }
    }
}