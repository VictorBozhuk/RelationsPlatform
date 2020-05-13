using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

using System;
using System.Reflection;

namespace DisciplinePicker.Persistence.Infrastructure
{
    public class DisciplinePickerDbContextFactory : IDesignTimeDbContextFactory<DisciplinePickerDatabaseContext>
    {
        public DisciplinePickerDatabaseContext CreateDbContext(string[] args)
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
            var optionsBuilder = new DbContextOptionsBuilder<DisciplinePickerDatabaseContext>();
            var connectionString = config.GetConnectionString(nameof(DisciplinePickerDatabaseContext));
            optionsBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly(Assembly.GetAssembly(typeof(DisciplinePickerDatabaseContext)).GetName().Name));
            return new DisciplinePickerDatabaseContext(optionsBuilder.Options);
        }
    }
}
