using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Wissance.WeatherControl.Data.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureSqlServerDbContext<TContext>(this IServiceCollection serviceCollection, string connectionString)
            where TContext : DbContext
        {
            serviceCollection.AddDbContext<TContext>(options => options.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
                .UseSqlServer(connectionString)
                .UseLazyLoadingProxies());
            return serviceCollection;
        }
    }
}
