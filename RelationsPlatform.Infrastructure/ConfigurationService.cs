using RelationsPlatform.Core;

using Microsoft.Extensions.Configuration;

namespace RelationsPlatform.Infrastructure
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IRootDirectoryProvider _rootDirectoryProvider;
        private readonly IEnvironmentProvider _environmentProvider;

        public ConfigurationService(IRootDirectoryProvider rootDirectoryProvider, IEnvironmentProvider environmentProvider)
        {
            _rootDirectoryProvider = rootDirectoryProvider;
            _environmentProvider = environmentProvider;
        }

        public IConfiguration GetConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(_rootDirectoryProvider.RootDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{_environmentProvider.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }
    }
}
