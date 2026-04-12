using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Task01.Models;
using Task01.Services;

namespace Task01.Controllers
{
    public class AIController : Controller
    {
        private readonly IAIService _aiService;

        public AIController(IAIService aiService)
        {
            _aiService = aiService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Ask(string question)
        {
            if (string.IsNullOrWhiteSpace(question))
            {
                ViewBag.Error = "Please enter a valid question.";
                return View("Index");
            }

            var request = new QuestionRequest { Question = question };
            var response = await _aiService.AskQuestionAsync(request);

            ViewBag.Question = question;
            ViewBag.Answer = response?.Answer;

            return View("Index");
        }
    }
}
