using Wissance.WeatherControl.Data.Entity;
using Wissance.WeatherControl.Dto;

namespace Wissance.WeatherControl.WebApi.Factory
{
    internal static class MeasureUnitFactory
    {
        public static MeasureUnitEntity Create(MeasureUnitDto dto)
        {
            MeasureUnitEntity entity = new MeasureUnitEntity()
            {
                Name = dto.Name,
                Description = dto.Description,
                Abbreviation = dto.Abbreviation
            };
            return entity;
        }

        public static MeasureUnitDto Create(MeasureUnitEntity entity)
        {
            return new MeasureUnitDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Abbreviation = entity.Abbreviation
            };
        }
    }
}