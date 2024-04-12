using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestNetMVC.Services;

namespace TestNetMVC.Areas.Admin.Controllers
{

  [Area("Admin")]
  [Route("Admin")]
  public class AdminController : Controller
  {

    private readonly AccountService accountService;
    public AdminController(AccountService _accountService)
    {
      accountService = _accountService;
    }

    [Route("Dashboard")]
    public IActionResult Dashboard()
    {
      return View("Dashboard");
    }


    [Route("Employees")]
    public IActionResult Employees()
    {
      return View("Employees");
    }

  }
}