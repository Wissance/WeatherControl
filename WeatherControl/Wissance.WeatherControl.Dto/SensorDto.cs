using System.Collections.Generic;

namespace Wissance.WeatherControl.Dto
{
    public class SensorDto : SensorMinData
    {
        public MeasureUnitDto MeasureUnit { get; set; }
        public IList<MeasurementDto> Measurements { get; set; }
    }
}