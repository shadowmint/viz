using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Service.Controllers.Data
{
  [Route("/api/data")]
  [AllowAnonymous]
  public class DataController : Controller
  {
    public IActionResult Index()
    {
      var random = new Random();
      var data = new List<double>();
      for (var i = 0; i < 50; i++)
      {
        data.Add(random.NextDouble());
      }

      return Json(new
      {
        data
      });
    }
  }
}