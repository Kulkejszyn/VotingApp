using Application.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetSection("ApplicationContext:ConnectionString").Value;
        services.AddDbContext<ApplicationContext>(options => { options.UseSqlite(connectionString, sqlOptions => { sqlOptions.MigrationsAssembly("Persistence"); }); });

        services.AddTransient<IApplicationContext, ApplicationContext>();
        return services;
    }
}