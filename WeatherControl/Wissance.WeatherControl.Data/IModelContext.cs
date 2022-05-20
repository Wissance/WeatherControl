using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Wissance.WeatherControl.Data.Entity;

namespace Wissance.WeatherControl.Data
{
    public interface IModelContext
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();

        DbSet<MeasurementsEntity> Measurements { get; set; }
        DbSet<StationEntity> Stations { get; set; }

    }
}
