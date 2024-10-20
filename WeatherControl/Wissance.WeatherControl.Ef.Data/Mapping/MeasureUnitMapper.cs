using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wissance.WeatherControl.Data.Entity;

namespace Wissance.WeatherControl.Data.Mapping
{
    internal static class MeasureUnitMapper
    {
        public static void Map(this EntityTypeBuilder<MeasureUnitEntity> builder)
        {
            builder.ToTable("MeasureUnit", "dbo");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name).IsRequired();
            builder.HasIndex(p => p.Name);

            builder.Property(p => p.Description).IsRequired(false);
            builder.Property(p => p.Abbreviation).IsRequired();
        }
    }
}