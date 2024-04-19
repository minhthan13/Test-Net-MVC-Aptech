using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestNetMVC.Services;

namespace TestNetMVC.Views.Shared.Components.RequestPaginate
{
  public class RequestPaginate : ViewComponent
  {
    private readonly AccountService accountService;
    public RequestPaginate(AccountService _accountService)
    {
      accountService = _accountService;
    }
    public IViewComponentResult Invoke(string username)
    {
      var user = accountService.FindByUsername(username);

      return View("RequestPaginate", new
      {
        account = new
        {
          id = user.Id,
          name = user.Username,
          role = user.Roles.FirstOrDefault().RoleName,
        }
      });
    }
  }
}