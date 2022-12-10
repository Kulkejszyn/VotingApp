using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Persistence;

public class DesignTimeContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
{
    public ApplicationContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<ApplicationContext>();

        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetSection("ApplicationContext:ConnectionString").Value;
        builder.UseSqlite(connectionString, b => { b.MigrationsAssembly("Persistence"); });

        return new ApplicationContext(builder.Options);
    }
}