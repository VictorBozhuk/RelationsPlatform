using RelationsPlatform.Core;
using RelationsPlatform.Persistence.Infrastructure;
using RelationsPlatform.Persistence.Infrastructure.Repository;
using RelationsPlatform.Persistence.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Reflection;

namespace RelationsPlatform.Infrastructure.DependencyResolver
{
    public class DependencyResolver
    {
        public static void RegisterDependencies(IServiceCollection services)
        {
            services.AddTransient<IEnvironmentProvider, EnvironmentProvider>();
            services.AddTransient<IRootDirectoryProvider, RootDirectoryProvider>();
            services.AddTransient<IConfigurationService, ConfigurationService>();

            services.AddDbContext<RelationsPlatformDataBaseContext>(optionsAction: (provider, optionsBuilder) => ConfigureSqlServer(provider, optionsBuilder));

            // підключити сюда сервіси
            services.AddTransient<IAccountStorage, AccountStorage>();
            services.AddTransient<IUserStorage, UserStorage>();
            services.AddTransient<IAbilityStorage, AbilityStorage>();
            services.AddTransient<IJobStorage, JobStorage>();
            services.AddTransient<IProfessionSkillStorage, ProfessionSkillStorage>();
            services.AddTransient<ISkillStorage, SkillStorage>();
            services.AddTransient<IEducationStorage, EducationStorage>();
            services.AddTransient<ISchoolStorage, SchoolStorage>();
            services.AddTransient<ICourseStorage, CourseStorage>();
            services.AddTransient<IHigherEducationStorage, HigherEducationStorage>();
            services.AddTransient<IRelationStorage, RelationStorage>();
        }

        private static void ConfigureSqlServer(IServiceProvider provider, DbContextOptionsBuilder optionsBuilder)
        {
            var configurationService = provider.GetService<IConfigurationService>();
            var connectionString = configurationService.GetConfiguration().GetConnectionString(nameof(RelationsPlatformDataBaseContext));

            optionsBuilder.UseSqlServer(connectionString, builder => builder.MigrationsAssembly(Assembly.GetAssembly(typeof(RelationsPlatformDataBaseContext)).GetName().Name));
        }
    }
}
