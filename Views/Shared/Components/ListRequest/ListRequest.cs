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
    public IViewComponentResult Invoke(int EmployeeId, int page)
    {
      var requests = new List<Request>();
      int PageSize = 5;
      var skip = (page - 1) * PageSize;
      var Take = PageSize;
      if (EmployeeId == 4)
      {
        requests = requestService.FindAll().Skip(skip).Take(Take).ToList();
      }
      else
      {
        requests = requestService.FindRequestEmployeeSubmitId(EmployeeId).Skip(skip).Take(Take).ToList();
      }

      return View("ListRequest", new { requests = requests, userId = EmployeeId, thisPage = page });



    }

  }
}