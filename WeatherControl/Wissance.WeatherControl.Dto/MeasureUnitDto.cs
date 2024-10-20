using System;

namespace Wissance.WeatherControl.Dto
{
    public class MeasureUnitDto
    {
        public MeasureUnitDto()
        {
            
        }
        
        public MeasureUnitDto(Guid id, string name, string description, string abbreviation)
        {
            Id = id;
            Name = name;
            Description = description;
            Abbreviation = abbreviation;
        }
        
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Abbreviation { get; set; }
    }
}