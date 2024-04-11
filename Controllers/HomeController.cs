using System.Diagnostics;
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
  public IActionResult Login(string username, string password)
  {
    if (accountService.Login(username, password))
    {

      TempData["msg"] = "valid !!!";

      HttpContext.Session.SetString("username", username);
      return RedirectToAction("Index", "DashboardAdmin", new { Area = "Admin" });
    }
    else
    {
      TempData["msg"] = "invalid";
      return View("Login");
    }

  }


  [Route("Ok")]
  public IActionResult Ok()
  {
    return View();
  }





}
