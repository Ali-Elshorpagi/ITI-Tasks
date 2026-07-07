using Chat.Web.Models;
using Chat.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Web.Controllers
{
    public class AuditController : Controller
    {
        private readonly ApiService _api;

        public AuditController(ApiService api)
        {
            _api = api;
        }

        public async Task<IActionResult> Logs()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("JwtToken")))
                return RedirectToAction("Login", "Account");

            var logs = await _api.GetAsync<List<AuditLogViewModel>>("api/audit");
            return View(logs ?? new List<AuditLogViewModel>());
        }
    }
}
