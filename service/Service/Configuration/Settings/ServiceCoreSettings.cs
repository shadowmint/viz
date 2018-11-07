using Microsoft.Extensions.Configuration;
using Service.Configuration.Infrastructure;

namespace Service.Configuration.Settings
{
  public class ServiceCoreSettings
  {
    public ServiceCoreSettings(IConfiguration configuration)
    {
      BindAddress = configuration.AsCollectionOfString("Core:BindAddress");
    }

    public string[] BindAddress { get; }
  }
}