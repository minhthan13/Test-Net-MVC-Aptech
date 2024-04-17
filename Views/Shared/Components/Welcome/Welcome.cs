using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestNetMVC.Services;

namespace TestNetMVC.Views.Shared.Components.Welcome
{
  public class Welcome : ViewComponent
  {

    public IViewComponentResult Invoke()
    {

      return View("Welcome");
    }
  }
}