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


      var roleName = account.Roles.First().RoleName;
      switch (roleName)
      {
        case "Admin":
          return RedirectToAction("Dashboard", "Admin", new { Areas = "Admin" });
        case "Staff":
          return RedirectToAction("Dashboard", "Employees", new { Areas = "Employees" });
      }
      return RedirectToAction("Dashboard");

    }
    else
    {
      TempData["msg"] = "invalid";
      return View("Login");
    }

  }


  public IActionResult Dashboard()
  {

    return ViewComponent("Dashboard");
  }

  [Route("logout")]
  public async Task<IActionResult> Logout()
  {

    await HttpContext.SignOutAsync();
    return View("login");
  }
}
