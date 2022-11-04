using Authorization.Api.Models;

namespace Authorization.Api.Services;

public interface IConfigurationsService
{
    FilePathsOptions GetFilePaths();
}
