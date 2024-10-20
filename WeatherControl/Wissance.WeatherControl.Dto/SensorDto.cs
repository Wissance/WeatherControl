using System;
using System.Collections.Generic;

namespace Wissance.WeatherControl.Dto
{
    public class SensorDto : SensorMinData
    {
        public SensorDto()
        {
            Measurements = new List<MeasurementDto>();
        }

        public SensorDto(Guid id, string name, string description, string latitude, string longitude,
            MeasureUnitDto measureUnit, IList<MeasurementDto> measurements)
           :base(id, name, description, latitude, longitude)
        {
            MeasureUnit = measureUnit;
            Measurements = measurements;
        }

        public MeasureUnitDto MeasureUnit { get; set; }
        public IList<MeasurementDto> Measurements { get; set; }
    }
}