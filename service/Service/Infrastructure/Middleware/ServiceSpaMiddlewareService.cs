using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Service.Infrastructure.Middleware
{
  internal class ServiceSpaMiddlewareService
  {
    private readonly ServiceSpaMiddlewareFileCache _cache;

    public ServiceSpaMiddlewareService()
    {
      _cache = new ServiceSpaMiddlewareFileCache();
    }

    public async Task<ServiceSpaMiddlewareServiceFileInfo> GetStaticFileInfo(PathString requestPath, string rootFolder, int expireSeconds)
    {
      var fullPath = GetFullPath(requestPath, rootFolder);
      if (fullPath == null)
      {
        return null;
      }

      if (_cache.Exists(fullPath, expireSeconds))
      {
        return _cache.Get(fullPath);
      }

      if (!File.Exists(fullPath))
      {
        return null;
      }

      await _cache.PopulateFrom(fullPath);
      return _cache.Get(fullPath);
    }

    private string GetFullPath(PathString requestPath, string rootFolder)
    {
      var partialPath = requestPath.ToString().TrimStart('/');
      var path = Path.GetFullPath(Path.Combine(rootFolder, partialPath));
      return !path.StartsWith(rootFolder) ? null : path;
    }
  }
}