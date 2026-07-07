using Chat.Web.Models;
using Chat.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Web.Controllers
{
    public class RoomsController : Controller
    {
        private readonly ApiService _api;

        public RoomsController(ApiService api)
        {
            _api = api;
        }

        private IActionResult RequireLogin()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("JwtToken")))
                return RedirectToAction("Login", "Account");
            return null!;
        }

        public async Task<IActionResult> Index()
        {
            var redirect = RequireLogin();
            if (redirect != null) return redirect;

            var rooms = await _api.GetAsync<List<RoomViewModel>>("api/rooms");
            return View(rooms ?? new List<RoomViewModel>());
        }

        [HttpGet]
        public IActionResult Create()
        {
            var redirect = RequireLogin();
            if (redirect != null) return redirect;
            return View(new CreateRoomViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRoomViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var (success, _) = await _api.PostAsync("api/rooms", new
            {
                name = model.Name,
                description = model.Description
            });

            if (!success)
            {
                ModelState.AddModelError("", "Could not create room.");
                return View(model);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _api.DeleteAsync($"api/rooms/{id}");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Join(int id)
        {
            await _api.PostAsync($"api/rooms/{id}/join", new { });
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Leave(int id)
        {
            await _api.PostAsync($"api/rooms/{id}/leave", new { });
            return RedirectToAction("Index");
        }
    }
}
