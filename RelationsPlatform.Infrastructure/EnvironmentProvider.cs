using RelationsPlatform.Core;

using System;

namespace RelationsPlatform.Infrastructure
{
    public class EnvironmentProvider : IEnvironmentProvider
    {
        public string EnvironmentName => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
    }
}
