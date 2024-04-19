using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TestNetMVC.Models;
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
      var requests = requestService.FindRequestEmployeeSubmitId(account.Id).OrderByDescending(r => r.EmployeeIdHandling);

      var employeeSupport = accountService.FindEmployeeSupport();

      return PartialView("~/Areas/Shared/Modals/_RequestSubmitModal.cshtml", new { requests, account, employeeSupport });
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
        notyf.Success("Change Suscces");
      }
      else
      {
        res = "Change Failed";
        notyf.Error("Change Failed");
      }
      return new JsonResult(new { status = res });
    }


    [Route("Filter")]
    public IActionResult Filter(string fromDate, string toDate, int priorityId)
    {
      var role = User.FindFirst(ClaimTypes.Role).Value;
      var username = User.FindFirst(ClaimTypes.Name).Value;
      var account = accountService.FindByUsername(username);
      var requests = requestService.FilterRequest(fromDate, toDate, priorityId, role, account.Id);


      return new JsonResult(requests);
    }


    [Route("getRequestPaginate")]
    public IActionResult getRequestPaginate(int userId, int page, int pageSize)
    {

      var skipItem = (page - 1) * pageSize;
      var requests = requestService.GetRequestPaginate(userId, skipItem, pageSize);
      var totalItems = requestService.GetTotalRequestCount(userId);
      var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
      return new JsonResult(new { requests, totalItems, totalPages });
    }

  }
}