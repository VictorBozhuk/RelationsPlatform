using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace RelationsPlatform.Persistence.Infrastructure
{
    public class CustomDesignTimeService : IDesignTimeServices
    {
        public void ConfigureDesignTimeServices(IServiceCollection serviceCollection)
                => serviceCollection.AddSingleton<IPluralizer, CustomPluralizer>();
    }
}
