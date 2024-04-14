using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using TestNetMVC.Services;



namespace TestNetMVC.Controllers;
[Route("home")]
public class HomeController : Controller
{

  private readonly AccountService accountService;


  public HomeController(AccountService _accountService)
  {
    accountService = _accountService;
  }

  [Route("")]
  [Route("~/")]
  [Route("Login")]
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
      switch (account.Roles.First().RoleName)
      {
        case "Admin":
          return RedirectToAction("Dashboard", "Admin", new { Area = "Admin" });
        case "Staff":
          return RedirectToAction("EmployeeRequest", "Employees", new { Area = "Employees" });
        case "Support Staff":
        default:
          return RedirectToAction("Dashboard", "Admin", new { Area = "Admin" });
      }
    }
    else
    {
      TempData["msg"] = "invalid";
      return View("Login");
    }

  }



  [Route("logout")]
  public async Task<IActionResult> Logout()
  {

    await HttpContext.SignOutAsync();
    return RedirectToAction("login");
  }
}
