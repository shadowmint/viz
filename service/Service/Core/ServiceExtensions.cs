using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.DependencyInjection;
using Service.Configuration.Settings;

namespace Service.Core
{
  public class ServiceExtensions
  {
    public const string CorsPolicy = "SERVICE";

    public ServiceExtensions AddCors(IServiceCollection services)
    {
      services.Configure<MvcOptions>(opt => { opt.Filters.Add(new CorsAuthorizationFilterFactory(CorsPolicy)); });
      services.AddCors(options => { options.AddPolicy(CorsPolicy, corsOptions => Configure(corsOptions, services)); });
      return this;
    }

    public void Configure(CorsPolicyBuilder options, IServiceCollection services)
    {
      var settings = new ServiceSettings().Auth;
      options.WithOrigins(settings.ValidAuthReferrers);
      options.WithHeaders("X-Requested-With", "Content-Type");
      options.WithMethods("POST");
      options.AllowCredentials();
    }
  }
}