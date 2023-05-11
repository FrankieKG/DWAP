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
    
    [HttpPost]
    public async Task<IActionResult> LoadData(IFormFile file)
    {

        if(file != null)
        {
            await repo.ReadFile(file);
        }
        else
        {
            return BadRequest("File can't be read");
        }

            return RedirectToAction("Index", "Home");
    }

    //Hjälpmetod för att generera Dictionary-fil. Ta bort den här innan vi överlämnar applikationen:
    public void Dictionary()
    {
        repo.GenerateNewDictionaries();
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult Privacy()
    {
      throw new NotImplementedException();
    }
  }
}