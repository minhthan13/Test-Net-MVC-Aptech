using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestNetMVC.Models;
using TestNetMVC.Services;

namespace TestNetMVC.Areas.Employees.Controllers
{

  [Authorize(Roles = "Staff")]
  [Area("Employees")]
  [Route("Employees")]
  public class EmployeesController : Controller
  {
    private readonly RequestService requestService;
    private readonly AccountService accountService;
    public EmployeesController(RequestService _requestService, AccountService _accountService)
    {
      requestService = _requestService;
      accountService = _accountService;
    }
    [Route("Dashboard")]
    public IActionResult Dashboard()
    {

      return ViewComponent("Dashboard");
    }
    [Route("")]
    [Route("EmployeeRequest")]
    public IActionResult EmployeeRequest()
    {
      var username = User.FindFirst(ClaimTypes.Name).Value;
      var user = accountService.FindByUsername(username);
      return ViewComponent("ListRequest", user.Id);
    }


    [HttpPost]
    [Route("AddNewRequest")]
    public IActionResult AddNewRequest(string Title, string Description, int EmployeeIdSubmit, int PriorityId)
    {
      var request = new Request()
      {
        Title = Title,
        Description = Description,
        EmployeeIdSubmit = EmployeeIdSubmit,
        PriorityId = PriorityId,
        SentDate = DateTime.Now,
        EmployeeIdHandling = null
      };


      if (requestService.AddNewRequest(request))
      {
        return RedirectToAction("EmployeeRequest");
      }

      return RedirectToAction("EmployeeRequest");



    }
  }
}