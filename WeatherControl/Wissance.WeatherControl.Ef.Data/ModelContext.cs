using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Wissance.WeatherControl.Data.Entity;
using Wissance.WeatherControl.Data.Mapping;
using Wissance.WebApiToolkit.Data.Entity;

namespace Wissance.WeatherControl.Data
{
    public class ModelContext : DbContext, IModelContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
             :base(options)
        {
        }

        public int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StationEntity>().Map();
            modelBuilder.Entity<MeasureUnitEntity>().Map();
            modelBuilder.Entity<SensorEntity>().Map();
            modelBuilder.Entity<MeasurementEntity>().Map();
        }

        public DbSet<StationEntity> Stations { get; set; }
        public DbSet<MeasureUnitEntity> MeasureUnits { get; set; }
        public DbSet<SensorEntity> Sensors { get; set; }
        public DbSet<MeasurementEntity> Measurements { get; set; }
    }
}
