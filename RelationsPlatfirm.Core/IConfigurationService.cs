using Microsoft.Extensions.Configuration;

namespace DisciplinePicker.Core
{
    public interface IConfigurationService
    {
        IConfiguration GetConfiguration();
    }
}
