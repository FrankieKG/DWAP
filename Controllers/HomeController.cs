using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
  public class HomeController : Controller
  {
    private readonly IRepository repo;

    public HomeController(IRepository repo)
    {
      this.repo = repo;
    }
    
    public ViewResult Index()
    {
      return View();
    }

    public ViewResult Mobilitetsstatistik()
    {
      return View();
    }
    
    public ViewResult Partnerskap()
    {
      return View();
    }
    
    public ViewResult Programstatistik()
    {
      return View();
    }
    
    public IActionResult Praktik()
    {
      return View();
    }
    
    public IActionResult Privacy()
    {
      return View();
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}