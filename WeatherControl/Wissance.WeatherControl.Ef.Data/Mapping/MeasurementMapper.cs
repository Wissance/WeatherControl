using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wissance.WeatherControl.Data.Entity;

namespace Wissance.WeatherControl.Data.Mapping
{
    internal static class MeasurementMapper
    {
        public static void Map(this EntityTypeBuilder<MeasurementEntity> builder)
        {
            builder.ToTable("Measurement", "dbo");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Value).IsRequired();
            builder.Property(p => p.SampleDate).IsRequired();
        }
    }
}
