using System;

namespace Wissance.WeatherControl.Dto
{
    public class MeasureUnitDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Abbreviation { get; set; }
    }
}