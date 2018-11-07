using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Service.Configuration.Settings;
using Service.Infrastructure.Middleware;
using IContainer = Autofac.IContainer;

namespace Service.Core
{
  public class ServiceApplication
  {
    private IContainer _container;

    public IServiceProvider ConfigureServices(IServiceCollection services)
    {
      // Services, etc.
      var builder = new ContainerBuilder();

      // Load asp.net core services
      services.AddRouting();
      services.AddMvc().AddFluentValidation();

      // Standard extensions
      new ServiceExtensions().AddCors(services);

      // Convert to autofac
      builder.Populate(services);
      _container = builder.Build();

      // Bind autofac as service provider
      return new AutofacServiceProvider(_container);
    }

    public void ConfigureApplication(IApplicationBuilder app, IHostingEnvironment env)
    {
      // Load settings
      var settings = new ServiceSettings();

      app.UseMvc();
      app.UseCors(ServiceExtensions.CorsPolicy);

      app.UseMiddleware<ServiceSpaMiddleware>(new ServiceSpaMiddlewareOptions()
      {
        StaticFolderRoot = settings.Folders.StaticAssetsFolder,
        IgnoreRoutes = new[] {"/api"},
        DefaultPath = "/index.html"
      });
    }
  }
}