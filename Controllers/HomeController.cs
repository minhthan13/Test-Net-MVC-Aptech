using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;


namespace TestNetMVC.Controllers;
[Route("home")]
public class HomeController : Controller
{
  private readonly ILogger<HomeController> _logger;



  public HomeController(ILogger<HomeController> logger)
  {
    _logger = logger;

  }
  [Route("")]
  [Route("~/")]
  [Route("index")]
  public IActionResult Index()
  {

    return View();
  }
  [Route("privacy")]

  public IActionResult Privacy()
  {
    return View();
  }


}
