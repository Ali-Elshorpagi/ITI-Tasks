using Microsoft.AspNetCore.Mvc;

namespace Chat.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("JwtToken")))
                return RedirectToAction("Login", "Account");

            return View();
        }
    }
}
