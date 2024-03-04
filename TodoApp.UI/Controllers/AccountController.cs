using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ToDoApp.UI.Models;
using ToDoApp.UI.Services;

namespace ToDoApp.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly ToDoAppService _toDoAppService;

        public AccountController(ToDoAppService toDoAppService)
        {
            _toDoAppService = toDoAppService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var responseDto = await _toDoAppService.Login(model);
            if (responseDto?.AccessToken == null)
            {
                ViewBag.Error = "Incorrect UserId or Password!";
                return View();
            }

            HttpContext.Session.SetString("JWTToken", responseDto.AccessToken);

            // Redirect to a protected page or home page
            return RedirectToAction("Index", "Home");
        }
    }
}
