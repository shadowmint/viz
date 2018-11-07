using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.StaticFiles;

namespace Service.Infrastructure.Middleware
{
  internal class ServiceSpaMiddlewareFileCache
  {
    private readonly ConcurrentDictionary<string, ServiceSpaMiddlewareServiceFileInfo> _cache =
      new ConcurrentDictionary<string, ServiceSpaMiddlewareServiceFileInfo>();

    private readonly FileExtensionContentTypeProvider _contentTypeProvider = new FileExtensionContentTypeProvider();

    public bool Exists(string fullPath, int expireSeconds)
    {
      var expiry = DateTimeOffset.Now - TimeSpan.FromSeconds(expireSeconds);
      return _cache.ContainsKey(fullPath) && _cache[fullPath].Created > expiry;
    }

    public ServiceSpaMiddlewareServiceFileInfo Get(string fullPath)
    {
      return Exists(fullPath, 1000) ? _cache[fullPath] : null;
    }

    public async Task PopulateFrom(string fullPath)
    {
      var filename = Path.GetFileName(fullPath);
      _cache[fullPath] = new ServiceSpaMiddlewareServiceFileInfo()
      {
        Filename = filename,
        MineType = MimeTypeFor(filename),
        Bytes = await File.ReadAllBytesAsync(fullPath),
        Created = DateTimeOffset.Now
      };
    }

    private string MimeTypeFor(string fileName)
    {
      if (!_contentTypeProvider.TryGetContentType(fileName, out var contentType))
      {
        contentType = "application/octet-stream";
      }

      return contentType;
    }
  }
}