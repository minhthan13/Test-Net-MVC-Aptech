using System.Diagnostics;
using System.Security.Claims;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using TestNetMVC.Helpers;
using TestNetMVC.Models;
using TestNetMVC.Services;



namespace TestNetMVC.Controllers;
[Route("home")]
public class HomeController : Controller
{
  private readonly AccountService accountService;
  private readonly IWebHostEnvironment environment;
  private readonly INotyfService notyf;

  public HomeController(AccountService _accountService, IWebHostEnvironment _environment, INotyfService _notyf)
  {
    accountService = _accountService;
    environment = _environment;
    notyf = _notyf;
  }

  [Route("")]
  [Route("~/")]
  [Route("Login")]
  [HttpGet]
  public IActionResult Login()
  {
    return View();
  }

  [HttpPost]
  [Route("Login")]
  public async Task<IActionResult> Login(string username, string password)
  {


    if (accountService.Login(username, password))
    {
      var account = accountService.FindByUsername(username);
      var claims = new List<Claim>();
      claims.Add(new Claim(ClaimTypes.Name, username));
      foreach (var role in account.Roles)
      {
        claims.Add(new Claim(ClaimTypes.Role, role.RoleName));
      }
      var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
      var claimsPrincial = new ClaimsPrincipal(claimsIdentity);
      await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincial);
      notyf.Success($"Welcome Back {account.Username.ToUpper()}");

      var roleName = account.Roles.First().RoleName;
      switch (roleName)
      {
        case "Admin":
          return RedirectToAction("Dashboard", "Admin", new { area = "Admin" });
        case "Staff":
          return RedirectToAction("Welcome", "Employees", new { area = "Employees" });
        case "Support Staff":
          return RedirectToAction("Welcome", "Support", new { area = "Support" });
        default:
          return RedirectToAction("Welcome");
      }
    }
    else
    {
      notyf.Error("Login Failed, Please try again !!");
      return RedirectToAction("Login");
    }

  }

  [Route("Welcome")]
  public IActionResult Welcome()
  {
    return ViewComponent("Welcome");
  }

  [HttpPost]
  [Route("EditAccount")]
  public IActionResult EditAccount(Employee employee, IFormFile Photo)
  {
    var account = accountService.FindByUsername(employee.Username);

    if (Photo != null)
    {
      var fileName = FileHelper.generateFileName(Photo.FileName);
      var path = Path.Combine(environment.WebRootPath, "images", fileName);
      using (var fileStream = new FileStream(path, FileMode.Create))
      {
        Photo.CopyTo(fileStream);
      };
      account.Photo = fileName;
    }
    account.Dob = employee.Dob;
    account.Fullname = employee.Fullname;

    if (accountService.EditAccount(account))
    {
      notyf.Success("Change Account Success !!!");
    }
    else
    {
      notyf.Error("Change Account Failed !!!");
    }
    return ViewComponent("Welcome");
  }

  [Route("logout")]
  public async Task<IActionResult> Logout()
  {
    await HttpContext.SignOutAsync();
    return View("login");
  }
}
