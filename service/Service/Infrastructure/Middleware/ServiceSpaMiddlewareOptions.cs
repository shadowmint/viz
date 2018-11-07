namespace Service.Infrastructure.Middleware
{
  public class ServiceSpaMiddlewareOptions
  {
    public string[] IgnoreRoutes { get; set; } = { };
    public string StaticFolderRoot { get; set; }
    public string DefaultPath { get; set; } = "/index.html";
    public int ExpireSeconds { get; set; } = 10;
  }
}