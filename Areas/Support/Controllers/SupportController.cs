using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestNetMVC.Services;

namespace TestNetMVC.Areas.Support.Controllers
{


  [Authorize(Roles = "Support Staff")]
  [Area("Support")]
  [Route("Support")]
  [Route("Support/Support")]
  public class SupportController : Controller
  {

    private readonly AccountService accountService;
    public SupportController(AccountService _accountService)
    {
      accountService = _accountService;
    }

    [Route("Welcome")]
    public IActionResult Welcome()
    {
      return ViewComponent("Welcome");
    }


    [Route("SupportRequest/{page?}")]
    public IActionResult ListSupportRequest(int? page)
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


  }
}