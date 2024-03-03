using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Net.Http.Headers;
using ToDoApp.UI.Models;

namespace ToDoApp.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri("https://localhost:7144");

            //    HttpResponseMessage response = await client.GetAsync("/ToDoItems");
            //    if (response.IsSuccessStatusCode)
            //    {
            //        string jsondata = await response.Content.ReadAsStringAsync();
            //        ViewBag.ToDoItemTitle = jsondata;
            //    }
            //}
            ViewBag.ToDoItemTitle = "homeeeeee";
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
