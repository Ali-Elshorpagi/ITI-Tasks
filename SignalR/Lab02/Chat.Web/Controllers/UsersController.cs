using Chat.Web.Models;
using Chat.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApiService _api;

        public UsersController(ApiService api)
        {
            _api = api;
        }

        public async Task<IActionResult> Online()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("JwtToken")))
                return RedirectToAction("Login", "Account");

            var users = await _api.GetAsync<List<UserViewModel>>("api/users");
            ViewBag.JwtToken = HttpContext.Session.GetString("JwtToken");
            ViewBag.CurrentUserId = HttpContext.Session.GetString("UserId");
            return View(users ?? new List<UserViewModel>());
        }
    }
}
