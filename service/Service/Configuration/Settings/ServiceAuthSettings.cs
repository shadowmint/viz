using System.Linq;
using Microsoft.Extensions.Configuration;
using Service.Configuration.Infrastructure;

namespace Service.Configuration.Settings
{
  public class ServiceAuthSettings
  {
    public ServiceAuthSettings(IConfiguration configuration)
    {
      ValidAuthReferrers = configuration
        .AsCollectionOfString("Auth:ValidAuthReferers")
        .Select(i => i.TrimEnd('/'))
        .ToArray();
    }

    public string[] ValidAuthReferrers { get; }
  }
}