using System;
using Wissance.WeatherControl.Data;
using Wissance.WeatherControl.Data.Entity;
using Wissance.WeatherControl.Dto;

namespace Wissance.WeatherControl.WebApi.Factory
{
    internal static class MeasureUnitFactory
    {
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
        
        public static MeasureUnitEntity Create(MeasureUnitDto dto, ModelContext context)
        {
            MeasureUnitEntity entity = new MeasureUnitEntity()
            {
                Name = dto.Name,
                Description = dto.Description,
                Abbreviation = dto.Abbreviation
            };
            return entity;
        }

        public static void Update(MeasureUnitDto data, Guid id, ModelContext context, MeasureUnitEntity entity)
        {
            entity.Name = data.Name;
            entity.Description = data.Description;
            entity.Abbreviation = data.Abbreviation;
        }
    }
}