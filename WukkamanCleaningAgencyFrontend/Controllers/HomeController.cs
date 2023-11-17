using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WukkamanCleaningAgencyFrontend.Models;

namespace WukkamanCleaningAgencyFrontend.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
     private readonly IHttpClientFactory _clientHandler;

    public HomeController(ILogger<HomeController> logger, IHttpClientFactory clientHandler)
    {
        _logger = logger;
         this._clientHandler = clientHandler;
    }

    public IActionResult Index()
    {
        
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult NavToShift(string EmployeeCode)
    {
        return RedirectToAction("Upsert" , "Shift", new { code = EmployeeCode});
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
