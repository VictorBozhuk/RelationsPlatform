using RelationsPlatform.Core;
using RelationsPlatform.Persistence.Infrastructure;
using RelationsPlatform.Persistence.Infrastructure.Repository;
using RelationsPlatform.Persistence.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Reflection;

namespace DisciplinePicker.Infrastructure.DependencyResolver
{
    public class DependencyResolver
    {
        public static void RegisterDependencies(IServiceCollection services)
        {
            services.AddTransient<IEnvironmentProvider, EnvironmentProvider>();
            services.AddTransient<IRootDirectoryProvider, RootDirectoryProvider>();
            services.AddTransient<IConfigurationService, ConfigurationService>();

            services.AddDbContext<DisciplinePickerDatabaseContext>(optionsAction: (provider, optionsBuilder) => ConfigureSqlServer(provider, optionsBuilder));

            services.AddTransient<IStudentStorage, StudentStorage>();
            services.AddTransient<IAdminStorage, AdminStorage>();
            services.AddTransient<IDisciplineStorage, DisciplineStorage>();
            services.AddTransient<ILecturerStorage, LecturerStorage>();
            services.AddTransient<IDisciplineChoiseStorage, DisciplineChoiseStorage>();
            services.AddTransient<IDisciplineAvailabilityStorage, DisciplineAvailabilityStorage>();
        }

        private static void ConfigureSqlServer(IServiceProvider provider, DbContextOptionsBuilder optionsBuilder)
        {
            var configurationService = provider.GetService<IConfigurationService>();
            var connectionString = configurationService.GetConfiguration()
                                   .GetConnectionString(nameof(DisciplinePickerDatabaseContext));

            optionsBuilder.UseSqlServer(connectionString, builder => builder.MigrationsAssembly(Assembly.GetAssembly(typeof(DisciplinePickerDatabaseContext)).GetName().Name));
        }

        private static void ConfigurePostgresDb(IServiceProvider provider, DbContextOptionsBuilder optionsBuilder)
        {
            var configurationService = provider.GetService<IConfigurationService>();
            var connectionString = configurationService.GetConfiguration()
                                   .GetConnectionString(nameof(DisciplinePickerDatabaseContext));

            optionsBuilder.UseNpgsql(connectionString, builder => builder.MigrationsAssembly(Assembly.GetAssembly(typeof(DisciplinePickerDatabaseContext)).GetName().Name));
        }
    }
}
