using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
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
    private readonly INotyfService notyf;
    public AjaxController(AccountService _accountService, RequestService _requestService, INotyfService _notyf)
    {
      accountService = _accountService;
      requestService = _requestService;
      notyf = _notyf;
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
      string message = "";
      var accounts = accountService.FindAll2();
      if (accountService.ActiveAcount(username, isChecked))
      {
        message = "Change Status Success !!";
        notyf.Success(message);

      }
      else
      {
        message = "Change Status Failed !!!";
        notyf.Success(message);
      }

      return new JsonResult(new { accounts, message });
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