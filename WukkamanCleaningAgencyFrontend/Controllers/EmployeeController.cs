using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using WukkamanCleaningAgencyFrontend.Models;
using System.Text;
using System.Net.Http.Headers;

namespace WukkamanCleaningAgencyFrontend.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IHttpClientFactory _clientHandler;
        public EmployeeController(IHttpClientFactory clientHandler)
        {
            this._clientHandler = clientHandler;
        }

        public async Task<IActionResult> Index()
        {
            HttpClient httpClient = _clientHandler.CreateClient("EmployeeAPI");

            HttpResponseMessage response = await httpClient.GetAsync("");

            List<Employee> productList = new();

            if(response != null){

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                productList = JsonConvert.DeserializeObject<List<Employee>>(content)!;
            }
            else
                return Problem("Error in Api response");

            return View(productList);
            }
            else 
                return View(productList);
        }


        public IActionResult Upsert(int id = 0)
        {

            if(id == 0)
                return View(new Employee());
            else
            {
                HttpResponseMessage employeeResponse = _clientHandler.CreateClient("EmployeeAPI").GetAsync($"{id}").Result;
                string employee = employeeResponse.Content.ReadAsStringAsync().Result;

                Employee view = JsonConvert.DeserializeObject<Employee>(employee)!;
                
                return View(view);
            }
        }


        [HttpPost]

        public IActionResult Upsert(Employee employee)
        {
           if (!ModelState.IsValid) return View(employee);

           string json = JsonConvert.SerializeObject(employee);

           StringContent data = new(json, Encoding.UTF8, "application/json");

           if (employee.Id == 0)
           {
               HttpResponseMessage response = _clientHandler.CreateClient("EmployeeAPI").PostAsync("", data).Result;

               if (response.IsSuccessStatusCode)
               {
                   return RedirectToAction("Index");
               }
               else
               {
                   ModelState.AddModelError(string.Empty, "Product creation failed");
                   return View(employee);
               }
           }
           else
           {
               HttpResponseMessage response = _clientHandler.CreateClient("EmployeeAPI").PutAsync($"{employee.Id}", data).Result;

               if (response.IsSuccessStatusCode)
               {
                   return RedirectToAction("Index");
               }
               else
               {
                   ModelState.AddModelError(string.Empty, "Product creation failed");
                   return View(employee);
               }
           }
        }

       

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            HttpClient httpClient = _clientHandler.CreateClient("EmployeeAPI");
            HttpResponseMessage response = await httpClient.DeleteAsync($"{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Student Delete failed");
                return RedirectToAction("Index");
            }
        }


    }
}
