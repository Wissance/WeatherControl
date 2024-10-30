using Wissance.WeatherControl.Dto;
using Wissance.WeatherControl.EdgeDb.Data.Entity;

namespace Wissance.WeatherControl.WebApi.V2.Factories
{
    public static class MeasureUnitFactory
    {
        public static MeasureUnitDto Create(MeasureUnitEntity entity)
        {
            return new MeasureUnitDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                Abbreviation = entity.Abbreviation
            };
        }
    }
}
