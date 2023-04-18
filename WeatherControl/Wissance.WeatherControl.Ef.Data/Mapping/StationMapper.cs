using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wissance.WeatherControl.Data.Entity;

namespace Wissance.WeatherControl.Data.Mapping
{
    internal static class StationMapper
    {
        public static void Map(this EntityTypeBuilder<StationEntity> builder)
        {
            builder.ToTable("Station", "dbo");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name).IsRequired();
            builder.HasIndex(p => p.Name);

            builder.HasMany(p => p.Measurements).WithOne(p => p.Station)
                .HasForeignKey(p => p.StationId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
