using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestNetMVC.Services;

namespace TestNetMVC.Views.Shared.Components.ListRequest
{
  public class ListRequest : ViewComponent
  {
    private readonly RequestService requestService;
    public ListRequest(RequestService _requestService)
    {
      requestService = _requestService;
    }
    public IViewComponentResult Invoke(int EmployeeId)
    {
      ViewBag.EmployeeId = EmployeeId;
      if (EmployeeId == 4)
      {

        return View("ListRequest", requestService.FindAll());
      }
      else
      {
        return View("ListRequest", requestService.FindRequestEmployeeSubmitId(EmployeeId));
      }


    }
  }
}