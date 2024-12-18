using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wissance.WeatherControl.Data.Entity;

namespace Wissance.WeatherControl.Data.Mapping
{
    internal static class SensorMapper
    {
        public static void Map(this EntityTypeBuilder<SensorEntity> builder)
        {
            builder.ToTable("Sensor", "dbo");
            builder.HasKey(p => p.Id);
            
            builder.Property(p => p.Name).IsRequired();
            builder.HasIndex(p => p.Name);
            
            builder.Property(p => p.Description).IsRequired(false);
            builder.Property(p => p.Latitude).IsRequired(false);
            builder.Property(p => p.Longitude).IsRequired(false);
            builder.Property(p => p.StationId).IsRequired();

            builder.HasOne(p => p.MeasureUnit).WithMany().OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(p => p.Measurements).WithOne(p => p.Sensor)
                .HasForeignKey(p => p.SensorId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}