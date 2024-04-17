using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TestNetMVC.Views.Shared.Components.Dashboard
{
  public class Dashboard : ViewComponent
  {
    public IViewComponentResult Invoke(string? a)
    {
      string b = "none";
      if (!string.IsNullOrEmpty(a))
      {
        b = a;
      }
      return View("Dashboard", b);
    }
  }
}