namespace Wissance.WeatherControl.Dto.V2
{
    public class SensorDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public IList<MeasurementDto> Measurement { get; set; }
    }
}