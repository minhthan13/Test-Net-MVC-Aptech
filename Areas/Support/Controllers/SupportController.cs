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


  }
}