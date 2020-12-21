using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using InventoryClientApp.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace InventoryClientApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
           
            return View();
        }

        public async Task<IActionResult> LoginUser(UserInfo user)
        {
            using (var httpClient = new HttpClient())
            {
                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:44327/api/token", stringContent))
                {
                    string token = await response.Content.ReadAsStringAsync();
                    if(token=="Invalid credentials")
                    {
                        ViewBag.Message = "Incorrect UserId or Password!";
                        return Redirect("~/Home/Index");
                    }
                    HttpContext.Session.SetString("JWToken", token);
                }

                return Redirect("~/Dashboard/Index");
            }
        }

        public IActionResult Logoff()
        {
            HttpContext.Session.Clear(); //clear token
            return Redirect("~/Home/Index");
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
