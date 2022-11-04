using Authorization.Api.Models;
using Microsoft.Extensions.Options;

namespace Authorization.Api.Services;

public class ConfigurationsService : IConfigurationsService
{
    private readonly IOptionsMonitor<FilePathsOptions> _filePathOptions;

    public ConfigurationsService(IOptionsMonitor<FilePathsOptions> filePathOptions)
    {
        _filePathOptions = filePathOptions;
    }

    public FilePathsOptions GetFilePaths()
    {
        return _filePathOptions.CurrentValue;
    }
}
