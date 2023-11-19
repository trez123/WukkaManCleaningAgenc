using WukkamanCleaningAgencyFrontend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.Encodings.Web;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace IdentityDemoFrontend.Controllers;

public class LoginController : Controller
{
    private readonly IHttpClientFactory _clientHandler;
    const string SESSION_AUTH = "IdentityDemo";
    public LoginController(IHttpClientFactory clienthandler)
    {
        this._clientHandler = clienthandler;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Welcome()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(User user)
    {
        if(ModelState.IsValid)
        {
            HttpClient httpClient = _clientHandler.CreateClient("AuthAPI");
            string json = JsonConvert.SerializeObject(user);
            StringContent data = new(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = httpClient.PostAsync("login", data).Result;

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                Dictionary<string, object> responseData = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseContent)!;

                if(responseData.ContainsKey("status") && responseData["status"].ToString() == "Success")
                {
                    if(responseData.ContainsKey("data") && responseData["data"] is JObject jObject)
                    {
                        // string token = responseData["data"].ToString()!;
                        string token = jObject.GetValue("result")!.ToString();
                        HttpContext.Session.SetString(SESSION_AUTH, token);
                    }
                    return RedirectToAction("Welcome");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Username or Password");
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "login failed, Invalid Username or Password");
                return RedirectToAction("Index");
            }
        }
        return View(user);
    }

    public IActionResult LogOut()
    {
        if(ModelState.IsValid)
        {
            HttpContext.Session.Remove(SESSION_AUTH);
            return RedirectToAction("Index", "Home");
        }

        return View();
    }
}