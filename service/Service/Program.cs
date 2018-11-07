﻿using Microsoft.AspNetCore.Hosting;
using Service.Configuration.Settings;

namespace Service
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var settings = new ServiceSettings();
      BuildWebHost(settings).Run();
    }

    private static IWebHost BuildWebHost(ServiceSettings settings)
    {
      return new WebHostBuilder()
        .UseKestrel()
        .UseContentRoot(settings.Folders.RootFolder)
        .UseStartup<Startup>()
        .UseUrls(settings.Core.BindAddress)
        .Build();
    }
  }
}