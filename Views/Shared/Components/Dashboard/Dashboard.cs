using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestNetMVC.Services;

namespace TestNetMVC.Views.Shared.Components.Dashboard
{
  public class Dashboard : ViewComponent
  {
    private readonly RequestService requestService;
    private readonly AccountService accountService;
    public Dashboard(RequestService _requestService, AccountService _accountService)
    {
      requestService = _requestService;
      accountService = _accountService;
    }
    public IViewComponentResult Invoke(string? a)
    {
      string b = "none";
      if (!string.IsNullOrEmpty(a))
      {
        b = a;
      }

      var EmployeesCount = accountService.FindAll().Count();
      var RequestsCount = requestService.FindAll().Count();
      return View("Dashboard", new { EmployeesCount, RequestsCount });
    }
  }
}