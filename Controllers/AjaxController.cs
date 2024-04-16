using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TestNetMVC.Services;

namespace TestNetMVC.Controllers
{
  [Route("ajax")]
  public class AjaxController : Controller
  {

    private readonly AccountService accountService;
    public AjaxController(AccountService _accountService)
    {
      accountService = _accountService;
    }
    [Route("findEmplpoyeeByUsername")]
    public IActionResult FindEmplpoyeeByUsername(string username)
    {

      var account = accountService.FindByUsername(username);

      return new JsonResult(account);
    }


    [Route("ModalEmployeePartial")]
    public IActionResult ModalEmployeePartial(string username)
    {

      var account = accountService.FindByUsername(username);

      return PartialView("~/Areas/Shared/Modals/_RequestSubmitModal.cshtml", account);
    }


  }
}