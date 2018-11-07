using System;

namespace Service.Infrastructure.Middleware
{
  internal static class ServiceSpaMiddlewareServiceFactory
  {
    private static readonly Lazy<ServiceSpaMiddlewareService> _instance = new Lazy<ServiceSpaMiddlewareService>();

    public static ServiceSpaMiddlewareService GetStaticFileService()
    {
      return _instance.Value;
    }
  }
}