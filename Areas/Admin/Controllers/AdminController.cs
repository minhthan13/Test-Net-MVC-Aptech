using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestNetMVC.Helpers;
using TestNetMVC.Models;
using TestNetMVC.Services;

namespace TestNetMVC.Areas.Admin.Controllers
{

  [Area("Admin")]
  [Route("Admin")]
  public class AdminController : Controller
  {

    private readonly AccountService accountService;
    private readonly RoleService roleService;
    private readonly IWebHostEnvironment environment;
    public AdminController(AccountService _accountService, RoleService _roleService, IWebHostEnvironment _environment)
    {
      accountService = _accountService;
      environment = _environment;
      roleService = _roleService;
    }

    [Route("Dashboard")]
    public IActionResult Dashboard()
    {
      return View("Dashboard");
    }


    [Route("Employees")]
    public IActionResult Employees()
    {
      return View("Employees");
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
      if (employee.Dob != null) employee.Dob = DateTime.Now;
      employee.Password = BCrypt.Net.BCrypt.HashPassword(employee.Password);
      employee.Status = false;
      employee.Photo = fileName;
      employee.Roles.Add(roleService.FindById(RoleId));

      if (accountService.AddNewEmployee(employee))
      {
        TempData["msg"] = "Add Employee success !!!";
        return RedirectToAction("Employees");
      }
      else
      {
        TempData["msg"] = "Add Employee Failed !!!";
        return RedirectToAction("Employees");

      }





    }

  }
}