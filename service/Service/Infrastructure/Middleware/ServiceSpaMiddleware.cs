using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Service.Infrastructure.Middleware
{
  public class ServiceSpaMiddleware
  {
    private readonly RequestDelegate _next;
    private ServiceSpaMiddlewareOptions _options;

    public ServiceSpaMiddleware(RequestDelegate next, ServiceSpaMiddlewareOptions options)
    {
      _next = next;
      _options = options;
    }


    public async Task Invoke(HttpContext context)
    {
      var requestPath = context.Request.Path;

      try
      {
        // Ignore API routes, etc.
        if (_options.IgnoreRoutes.Any(i => requestPath.StartsWithSegments(i)))
        {
          await _next(context);
          return;
        }

        // Match static files in the root folder
        var fileInfo = await GetStaticFileInfo(requestPath, _options.StaticFolderRoot, _options.DefaultPath);
        if (fileInfo != null)
        {
          context.Response.StatusCode = 200;
          context.Response.ContentType = fileInfo.MineType;
          await context.Response.Body.WriteAsync(fileInfo.Bytes);
          return;
        }
      }
      catch (Exception)
      {
        // Ignore
      }

      await _next(context);
    }

    private async Task<ServiceSpaMiddlewareServiceFileInfo> GetStaticFileInfo(PathString requestPath, string rootFolder, string defaultPath)
    {
      var service = ServiceSpaMiddlewareServiceFactory.GetStaticFileService();
      var info = await service.GetStaticFileInfo(requestPath, rootFolder, _options.ExpireSeconds);
      if (info != null)
      {
        return info;
      }

      return await service.GetStaticFileInfo(defaultPath, rootFolder, _options.ExpireSeconds);
    }
  }
}