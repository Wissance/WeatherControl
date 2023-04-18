using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wissance.WeatherControl.Data.Entity;

namespace Wissance.WeatherControl.Data.Mapping
{
    internal static class MeasurementsMapper
    {
        public static void Map(this EntityTypeBuilder<MeasurementsEntity> builder)
        {
            builder.ToTable("Measurement", "dbo");
            builder.HasKey(p => p.Id);
        }
    }
}
