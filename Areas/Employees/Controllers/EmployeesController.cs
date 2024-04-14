using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TestNetMVC.Areas.Employees.Controllers
{
  [Area("Employees")]
  [Route("Employees")]
  public class EmployeesController : Controller
  {


    public EmployeesController()
    {

    }
    [Route("")]
    [Route("Index")]
    public IActionResult Index()
    {
      return View();
    }
  }
}