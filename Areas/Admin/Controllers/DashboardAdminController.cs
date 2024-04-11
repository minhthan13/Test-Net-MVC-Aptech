using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TestNetMVC.Areas.Admin.Controllers
{

  [Area("Admin")]
  [Route("Admin/Dashboard")]
  public class DashboardAdminController : Controller
  {


    [Route("Index")]

    public IActionResult Index()
    {
      return View();
    }

  }
}