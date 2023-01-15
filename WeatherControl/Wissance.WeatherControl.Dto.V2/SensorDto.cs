namespace Wissance.WeatherControl.Dto.V2
{
    public class SensorDto
    {
        //todo(UMV): add constructor with parameters
        public SensorDto()
        {
            Measurement = new List<MeasurementDto>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public IList<MeasurementDto> Measurement { get; set; }
    }
}