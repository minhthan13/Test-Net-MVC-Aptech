using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestNetMVC.Services;

namespace TestNetMVC.Views.Shared.Components.Information
{
  public class Information : ViewComponent
  {
    private readonly AccountService accountService;
    public Information(AccountService _accountService)
    {
      accountService = _accountService;
    }
    public IViewComponentResult Invoke(string username)
    {

      return View("Information", accountService.FindByUsername(username));
    }
  }
}