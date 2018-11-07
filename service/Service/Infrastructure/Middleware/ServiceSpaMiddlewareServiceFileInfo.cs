using System;

namespace Service.Infrastructure.Middleware
{
  internal class ServiceSpaMiddlewareServiceFileInfo
  {
    public string Filename { get; set; } = "index.html";
    public string MineType { get; set; } = "text/html";
    public byte[] Bytes { get; set; } = { };
    public DateTimeOffset Created { get; set; }
  }
}