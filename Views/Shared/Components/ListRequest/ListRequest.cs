using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestNetMVC.Helpers;
using TestNetMVC.Models;
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
    public IViewComponentResult Invoke(int EmployeeId, string roleName, int page)
    {
      var requests = new List<Request>();


      int PageSize = 5;
      var Take = PageSize;
      var skip = (page - 1) * PageSize;
      switch (roleName)
      {
        case "Admin":
          requests = requestService.FindAll().Skip(skip).Take(Take).ToList();
          break;

        case "Staff":
          requests = requestService.FindRequestEmployeeSubmitId(EmployeeId).Skip(skip).Take(Take).ToList();
          break;

        case "Support Staff":
          requests = requestService.FindRequestEmployeeHandleId(EmployeeId).Skip(skip).Take(Take).ToList();
          break;
      }

      return View("ListRequest", new { requests, userId = EmployeeId, thisPage = page });



    }

  }
}