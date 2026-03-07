using Microsoft.AspNetCore.Mvc;

namespace Task.Controllers
{
    public class UtilityController : Controller
    {
        public IActionResult ShowAddForm()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(int x, int y)
        {
            int result = x + y;
            ViewBag.X = x;
            ViewBag.Y = y;
            ViewBag.Result = result;
            ViewBag.Operation = "Add";
            return View("Result");
        }
        [HttpPost]
        public IActionResult Subtract(int x, int y)
        {
            int result = x - y;
            ViewBag.X = x;
            ViewBag.Y = y;
            ViewBag.Result = result;
            ViewBag.Operation = "Sub";
            return View("Result");
        }
    }
}
