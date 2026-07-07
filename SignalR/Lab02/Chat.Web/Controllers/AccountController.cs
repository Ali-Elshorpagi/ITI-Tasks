using Chat.Web.Models;
using Chat.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Chat.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApiService _api;

        public AccountController(ApiService api)
        {
            _api = api;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var (success, content) = await _api.PostAsync("api/auth/login", new
            {
                emailOrUsername = model.EmailOrUsername,
                password = model.Password
            });

            if (!success)
            {
                ModelState.AddModelError("", "Invalid email or password.");
                return View(model);
            }

            // Save the JWT token and user info in session
            var data = JsonSerializer.Deserialize<JsonElement>(content);
            HttpContext.Session.SetString("JwtToken", data.GetProperty("token").GetString() ?? "");
            HttpContext.Session.SetString("UserId", data.GetProperty("userId").GetString() ?? "");
            HttpContext.Session.SetString("UserName", data.GetProperty("userName").GetString() ?? "");
            HttpContext.Session.SetString("DisplayName", data.GetProperty("displayName").GetString() ?? "");

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var (success, content) = await _api.PostAsync("api/auth/register", new
            {
                userName = model.UserName,
                displayName = model.DisplayName,
                email = model.Email,
                password = model.Password
            });

            if (!success)
            {
                ModelState.AddModelError("", "Registration failed. Try a different username or email.");
                return View(model);
            }

            var data = JsonSerializer.Deserialize<JsonElement>(content);
            HttpContext.Session.SetString("JwtToken", data.GetProperty("token").GetString() ?? "");
            HttpContext.Session.SetString("UserId", data.GetProperty("userId").GetString() ?? "");
            HttpContext.Session.SetString("UserName", data.GetProperty("userName").GetString() ?? "");
            HttpContext.Session.SetString("DisplayName", data.GetProperty("displayName").GetString() ?? "");

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _api.PostAsync("api/auth/logout", new { });
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
