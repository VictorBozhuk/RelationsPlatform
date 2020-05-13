using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

using System;
using System.Reflection;

namespace RelationsPlatform.Persistence.Infrastructure
{
    public class RelationsPlatformDbContextFactory : IDesignTimeDbContextFactory<RelationsPlatformDataBaseContext>
    {
        public RelationsPlatformDataBaseContext CreateDbContext(string[] args)
        {
            // Get environment
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            // Build config
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            // Get connection string
            var optionsBuilder = new DbContextOptionsBuilder<RelationsPlatformDataBaseContext>();
            var connectionString = config.GetConnectionString(nameof(RelationsPlatformDataBaseContext));
            optionsBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly(Assembly.GetAssembly(typeof(RelationsPlatformDataBaseContext)).GetName().Name));
            return new RelationsPlatformDataBaseContext(optionsBuilder.Options);
        }
    }
}
