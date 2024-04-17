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
    private readonly RequestService requestService;
    public AjaxController(AccountService _accountService, RequestService _requestService)
    {
      accountService = _accountService;
      requestService = _requestService;
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
      var requests = requestService.FindRequestEmployeeSubmitId(account.Id).OrderByDescending(r =>
  r.EmployeeIdHandling);

      return PartialView("~/Areas/Shared/Modals/_RequestSubmitModal.cshtml", new { requests, account });
    }



    [Route("ActiveAccount")]
    public IActionResult ActiveAccount(string username, bool isChecked)
    {
      string res = "";
      if (accountService.ActiveAcount(username, isChecked))
      {
        res = "Active Suscces";
      }
      else
      {
        res = "Active Failed";
      }
      return new JsonResult(new { status = res });
    }


    [Route("ChangeHandleRequest")]
    public IActionResult ChangeHandleRequest(int ESuppId, int requestId)
    {
      string res = "";
      if (requestService.ChangeHandleRequest(ESuppId, requestId))
      {
        res = "Change Suscces";
      }
      else
      {
        res = "Change Failed";
      }
      return new JsonResult(new { status = res });
    }





  }
}