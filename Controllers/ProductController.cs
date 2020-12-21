using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using InventoryClientApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace InventoryClientApp.Controllers
{
    public class ProductController : Controller
    {
        public static string baseUrl = "https://localhost:44327/api/products/";
        public async Task<IActionResult> Index()
        {
            var products = await GetProducts();
            return View(products);
        }
        
        [HttpGet]
        public async Task<List<Product>> GetProducts()
        {
            //use the access token to call a protected web API.
            var accessToken = HttpContext.Session.GetString("JWToken");
            var url = baseUrl;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            string jsonStr = await client.GetStringAsync(url);

            var res = JsonConvert.DeserializeObject<List<Product>>(jsonStr).ToList();
            return res;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProdId, ProdName, Category, UnitPrice, StockQty")] Product product)
        {
            //use the access token to call a protected web API.
            var accessToken = HttpContext.Session.GetString("JWToken");
            var url = baseUrl;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var stringContent = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
            await client.PostAsync(url, stringContent);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accessToken = HttpContext.Session.GetString("JWToken");
            var url = baseUrl + id;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            string jsonStr = await client.GetStringAsync(url);
            var res = JsonConvert.DeserializeObject<Product>(jsonStr);

            if (res == null)
            {
                return NotFound();
            }
            return View(res);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProdId, ProdName, Category, UnitPrice, StockQty")] Product product)
        {
            if (id != product.ProdId)
            {
                return NotFound();
            }

            //use the access token to call a protected web API.
            var accessToken = HttpContext.Session.GetString("JWToken");
            var url = baseUrl + id;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var stringContent = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
            await client.PostAsync(url, stringContent);

            return RedirectToAction(nameof(Index));
        }

       
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accessToken = HttpContext.Session.GetString("JWToken");
            var url = baseUrl + id;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            string jsonStr = await client.GetStringAsync(url);
            var res = JsonConvert.DeserializeObject<Product>(jsonStr);

            if (res == null)
            {
                return NotFound();
            }
            return View(res);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {

            //use the access token to call a protected web API.
            var accessToken = HttpContext.Session.GetString("JWToken");
            var url = baseUrl + id;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            await client.DeleteAsync(url);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accessToken = HttpContext.Session.GetString("JWToken");
            var url = baseUrl + id;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            string jsonStr = await client.GetStringAsync(url);
            var res = JsonConvert.DeserializeObject<Product>(jsonStr);

            if (res == null)
            {
                return NotFound();
            }
            return View(res);
        }

    }
}
