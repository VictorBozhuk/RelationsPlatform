using Microsoft.Extensions.Configuration;

namespace RelationsPlatform.Core
{
    public interface IConfigurationService
    {
        IConfiguration GetConfiguration();
    }
}
