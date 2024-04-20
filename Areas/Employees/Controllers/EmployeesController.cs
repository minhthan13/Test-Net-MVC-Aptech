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

    [Route("Requests")]
    public IActionResult RequestsPaginate()
    {
      var username = User.FindFirst(ClaimTypes.Name).Value;
      return ViewComponent("RequestPaginate", new { username });

    }



    [HttpPost]
    [Route("AddNewRequest")]
    public IActionResult AddNewRequest(Request request, int PriorityId)
    {
      request.SentDate = DateTime.Now;
      request.PriorityId = PriorityId;
      if (requestService.AddNewRequest(request))
      {
        notyf.Success("Add New Request Success !!!");
        return RedirectToAction("Requests");
      }
      else
      {
        notyf.Error("Add New Request Failed !!!");
        return RedirectToAction("Requests");
      }
    }


    [HttpPost]
    [Route("ChangePassword")]
    public IActionResult ChangePassword(string username, string oPassword, string nPassword)
    {
      var account = accountService.FindByUsername(username);
      if (BCrypt.Net.BCrypt.Verify(oPassword, account.Password))
      {
        account.Password = BCrypt.Net.BCrypt.HashPassword(nPassword);
        if (accountService.EditAccount(account))
        {
          notyf.Success("Change Password Success !!!");
          return RedirectToAction("Information", new { username });
        }
        else
        {
          notyf.Error("Change Password Failed");
          return RedirectToAction("Information", new { username });
        }
      }

      return RedirectToAction("Information", new { username });
    }



  }
}