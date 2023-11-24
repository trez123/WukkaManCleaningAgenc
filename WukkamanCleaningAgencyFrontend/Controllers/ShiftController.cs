using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using WukkamanCleaningAgencyFrontend.Models;
using System.Text;
using System.Net.Http.Headers;
using System;

namespace WukkamanCleaningAgencyFrontend.Controllers
{
    public class ShiftController : Controller
    {
        private readonly IHttpClientFactory _clientHandler;
        public ShiftController(IHttpClientFactory clientHandler)
        {
            this._clientHandler = clientHandler;
        }

        public async Task<IActionResult> Index()
        {
            HttpClient httpClient = _clientHandler.CreateClient("ShiftAPI");

            HttpResponseMessage response = await httpClient.GetAsync("");

            List<Shift> productList = new();

            if(response != null){

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                productList = JsonConvert.DeserializeObject<List<Shift>>(content)!;
            }
            else
                return Problem("Error in Api response");

            return View(productList);
            }
            else 
                return View(productList);
        }


        public IActionResult Upsert(string code)
        {

            if(code == null)
                return View(new Shift());
            else
            {
                HttpResponseMessage EmployeeResponse = _clientHandler.CreateClient("EmployeeAPI").GetAsync($"GetEmployeeByCode?code={code}").Result;
                string employeeString = EmployeeResponse.Content.ReadAsStringAsync().Result;
                Employee employee = JsonConvert.DeserializeObject<Employee>(employeeString)!;

                Shift view = new()
                {
                    ShiftStart = DateTime.Now,
                    ShiftEnd = DateTime.Now,
                    Employee = employee
                };

                return View(view);
            }
        }


        [HttpPost]
        public IActionResult Upsert(Shift shift)
        {
            HttpResponseMessage EmployeeResponse = _clientHandler.CreateClient("EmployeeAPI").GetAsync($"{shift.Employee!.Id}").Result;
            string employeeString = EmployeeResponse.Content.ReadAsStringAsync().Result;
            Employee employee = JsonConvert.DeserializeObject<Employee>(employeeString)!;
            shift.Employee = employee;
            shift.Employee.Id = 0;

           string json = JsonConvert.SerializeObject(shift);

           StringContent data = new(json, Encoding.UTF8, "application/json");

           if (shift.Id == 0)
           {
               HttpResponseMessage response = _clientHandler.CreateClient("ShiftAPI").PostAsync("", data).Result;

               if (response.IsSuccessStatusCode)
               {
                   return RedirectToAction("Index","Home");
               }
               else
               {
                   ModelState.AddModelError(string.Empty, "Shift log failed");
                   return View(shift);
               }
           }
           else
           {
               HttpResponseMessage response = _clientHandler.CreateClient("ShiftAPI").PutAsync($"{shift.Id}", data).Result;

               if (response.IsSuccessStatusCode)
               {
                   return RedirectToAction("Index");
               }
               else
               {
                   ModelState.AddModelError(string.Empty, "Shift edit log failed");
                   return View(shift);
               }
           }
        }

       

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            HttpClient httpClient = _clientHandler.CreateClient("ShiftAPI");
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
