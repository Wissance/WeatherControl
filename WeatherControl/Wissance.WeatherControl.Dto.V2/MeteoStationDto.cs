namespace Wissance.WeatherControl.Dto.V2
{
    public class MeteoStationDto
    {
        public MeteoStationDto()
        {
            Sensors = new List<SensorDto>();
        }

        public Guid Id { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public IList<SensorDto> Sensors { get; set; }
    }
}