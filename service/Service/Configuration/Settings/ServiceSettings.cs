using System;
using Microsoft.Extensions.Configuration;

namespace Service.Configuration.Settings
{
  public class ServiceSettings
  {
    public ServiceCoreSettings Core => _core.Value;
    
    public ServiceAuthSettings Auth => _auth.Value;

    public ServiceFolderSettings Folders => StaticFolders.Value;

    private readonly Lazy<ServiceCoreSettings> _core = new Lazy<ServiceCoreSettings>(() => new ServiceCoreSettings(Config.Value));
    
    private readonly Lazy<ServiceAuthSettings> _auth = new Lazy<ServiceAuthSettings>(() => new ServiceAuthSettings(Config.Value));

    private static readonly Lazy<ServiceFolderSettings> StaticFolders = new Lazy<ServiceFolderSettings>(() => new ServiceFolderSettings());

    private static readonly Lazy<IConfiguration> Config = new Lazy<IConfiguration>(() =>
    {
      var builder = new ConfigurationBuilder()
        .SetBasePath(StaticFolders.Value.RootFolder)
        .AddJsonFile("appsettings.json")
        .AddEnvironmentVariables();

      return builder.Build();
    });
  }
}