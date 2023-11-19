using WukkamanCleaningAgencyFrontend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.Encodings.Web;
using System.Net.Http.Headers;

namespace WukkamanCleaningAgencyFrontend.Controllers;

public class RegisterController : Controller
{
    private readonly IHttpClientFactory _clientHandler;
    public RegisterController(IHttpClientFactory clienthandler)
    {
        this._clientHandler = clienthandler;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(User user)
    {
        if(ModelState.IsValid)
        {
            HttpClient httpClient = _clientHandler.CreateClient("AuthAPI");
            string json = JsonConvert.SerializeObject(user);
            StringContent data = new(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = httpClient.PostAsync("register", data).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "register failed");
                return View(user);
            }
        }
        return View(user);
    }
}