using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using Task01.Models;

namespace Task01.Controllers
{
    public class HomeController : Controller
    {
        private readonly RagOptions _ragOptions;
        private readonly VectorStoreOptions _vectorStoreOptions;

        public HomeController(IOptions<RagOptions> ragOptions, IOptions<VectorStoreOptions> vectorStoreOptions)
        {
            _ragOptions = ragOptions.Value;
            _vectorStoreOptions = vectorStoreOptions.Value;
        }

        public IActionResult Index()
        {
            ViewData["ChunkStrategy"] = _ragOptions.ChunkStrategy;
            ViewData["ChunkSize"] = _ragOptions.ChunkSize;
            ViewData["ChunkOverlap"] = _ragOptions.ChunkOverlap;
            ViewData["VectorProvider"] = _vectorStoreOptions.Provider;
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
