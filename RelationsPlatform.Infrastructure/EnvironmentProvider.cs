using DisciplinePicker.Core;

using System;

namespace DisciplinePicker.Infrastructure
{
    public class EnvironmentProvider : IEnvironmentProvider
    {
        public string EnvironmentName => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
    }
}
