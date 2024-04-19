using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
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
  [Route("Employees/Employees")]
  public class EmployeesController : Controller
  {
    private readonly RequestService requestService;
    private readonly AccountService accountService;
    private readonly INotyfService notyf;
    public EmployeesController(RequestService _requestService, AccountService _accountService, INotyfService _notyf)
    {
      requestService = _requestService;
      accountService = _accountService;
      notyf = _notyf;
    }
    [Route("Welcome")]
    public IActionResult Welcome()
    {
      return ViewComponent("Welcome");
    }

    [Route("Information/{username}")]
    public IActionResult Information(string username)
    {

      return ViewComponent("Information", username);
    }


    [Route("EmployeesRequest")]
    [Route("EmployeesRequest/{page?}")]
    public IActionResult EmployeesRequest(int? page)
    {
      if (page == null)
      {
        page = 1;
      }
      var username = User.FindFirst(ClaimTypes.Name).Value;
      var roleName = User.FindFirst(ClaimTypes.Role).Value;
      var user = accountService.FindByUsername(username);
      return ViewComponent("ListRequest", new { EmployeeId = user.Id, roleName, page });
    }


    [HttpPost]
    [Route("AddNewRequest")]
    public IActionResult AddNewRequest(Request request)
    {
      request.SentDate = DateTime.Now;
      if (requestService.AddNewRequest(request))
      {
        notyf.Success("Add New Request Success !!!");
        return RedirectToAction("EmployeesRequest");
      }
      else
      {
        notyf.Error("Add New Request Failed !!!");
        return RedirectToAction("EmployeesRequest");
      }
    }


    [Route("RequestsPaginate")]
    public IActionResult RequestsPaginate()
    {
      var username = User.FindFirst(ClaimTypes.Name).Value;
      return ViewComponent("RequestPaginate", new { username });

    }
  }
}