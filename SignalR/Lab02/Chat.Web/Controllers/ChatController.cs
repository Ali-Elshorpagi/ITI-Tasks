using Chat.Web.Models;
using Chat.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Web.Controllers
{
    public class ChatController : Controller
    {
        private readonly ApiService _api;

        public ChatController(ApiService api)
        {
            _api = api;
        }

        private IActionResult? RequireLogin()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("JwtToken")))
                return RedirectToAction("Login", "Account");
            return null;
        }

        public async Task<IActionResult> Room(int id)
        {
            var redirect = RequireLogin();
            if (redirect != null) return redirect;

            var room = await _api.GetAsync<RoomViewModel>($"api/rooms/{id}");
            if (room == null) return NotFound();

            var messages = await _api.GetAsync<List<MessageViewModel>>($"api/messages/room/{id}");

            ViewBag.Room = room;
            ViewBag.Messages = messages ?? new List<MessageViewModel>();
            ViewBag.CurrentUserId = HttpContext.Session.GetString("UserId");
            ViewBag.JwtToken = HttpContext.Session.GetString("JwtToken");
            return View();
        }

        public async Task<IActionResult> Private(string userId)
        {
            var redirect = RequireLogin();
            if (redirect != null) return redirect;

            var otherUser = await _api.GetAsync<UserViewModel>($"api/users/{userId}");
            if (otherUser == null) return NotFound();

            var messages = await _api.GetAsync<List<MessageViewModel>>($"api/messages/private/{userId}");

            ViewBag.OtherUser = otherUser;
            ViewBag.Messages = messages ?? new List<MessageViewModel>();
            ViewBag.CurrentUserId = HttpContext.Session.GetString("UserId");
            ViewBag.JwtToken = HttpContext.Session.GetString("JwtToken");
            return View();
        }
    }
}
