using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestNetMVC.Helpers;
using TestNetMVC.Models;
using TestNetMVC.Services;


namespace TestNetMVC.Areas.Admin.Controllers
{
  [Authorize(Roles = "Admin")]

  [Area("Admin")]
  [Route("Admin")]
  [Route("Admin/Admin")]
  public class AdminController : Controller
  {

    private readonly AccountService accountService;
    private readonly RoleService roleService;
    private readonly IWebHostEnvironment environment;
    private readonly INotyfService notyf;
    public AdminController(AccountService _accountService, RoleService _roleService, IWebHostEnvironment _environment, INotyfService _notyf)
    {
      accountService = _accountService;
      environment = _environment;
      roleService = _roleService;
      notyf = _notyf;
    }

    [Route("Dashboard")]
    public IActionResult Dashboard()
    {
      return ViewComponent("Dashboard");
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

    [Route("Employees")]
    public IActionResult Employees()
    {
      return View("Employees");
    }
    [Route("Requests")]
    public IActionResult RequestsPaginate()
    {
      var username = User.FindFirst(ClaimTypes.Name).Value;
      return ViewComponent("RequestPaginate", new { username });

    }
    [HttpPost]
    [Route("AddNewEmployee")]
    public IActionResult AddNewEmployee(Employee employee, int RoleId, IFormFile Photo)
    {
      var fileName = FileHelper.generateFileName(Photo.FileName);
      var path = Path.Combine(environment.WebRootPath, "images", fileName);
      using (var fileStream = new FileStream(path, FileMode.Create))
      {
        Photo.CopyTo(fileStream);
      };
      employee.Dob = DateTime.Now;
      employee.Password = BCrypt.Net.BCrypt.HashPassword(employee.Password);
      employee.Status = false;
      employee.Photo = fileName;
      employee.Roles.Add(roleService.FindById(RoleId));

      if (accountService.AddNewEmployee(employee))
      {

        notyf.Success("Add New Employee success !!!");
        return RedirectToAction("Employees");
      }
      else
      {
        notyf.Error("Add New Employee Failed !!!");
        return RedirectToAction("Employees");

      }
    }



    [HttpPost]
    [Route("ChangePassword")]
    public IActionResult ChangePassword(string username, string oPassword, string nPassword)
    {
      var account = accountService.FindByUsername(username);
      if (BCrypt.Net.BCrypt.Verify(oPassword, account.Password))
      {
        account.Password = BCrypt.Net.BCrypt.HashPassword(nPassword);
        if (accountService.EditAccount(account))
        {
          notyf.Success("Change Password Success !!!");
          return RedirectToAction("Information", new { username });
        }
        else
        {
          notyf.Error("Change Password Failed");
          return RedirectToAction("Information", new { username });
        }
      }

      return RedirectToAction("Information", new { username });
    }






  }
}