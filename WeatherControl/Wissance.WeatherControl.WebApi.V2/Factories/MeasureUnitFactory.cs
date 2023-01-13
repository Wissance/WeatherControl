using Wissance.WeatherControl.Dto.V2;
using Wissance.WeatherControl.GraphData.Entity;

namespace Wissance.WeatherControl.WebApi.V2.Factories
{
    public static class MeasureUnitFactory
    {
        public static MeasureUnitDto Create(MeasureUnitEntity entity)
        {
            return new MeasureUnitDto()
            {
                Name = entity.Name,
                Abbreviation = entity.Abbreviation
            };
        }
    }
}
