using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Wissance.WeatherControl.Data.Entity;

namespace Wissance.WeatherControl.Data
{
    public class ModelContext : DbContext, IModelContext
    {
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

        public DbSet<MeasurementsEntity> Measurements { get; set; }
        public DbSet<StationEntity> Stations { get; set; }
    }
}
