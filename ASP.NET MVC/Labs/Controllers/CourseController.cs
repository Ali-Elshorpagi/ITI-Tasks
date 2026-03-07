using Microsoft.AspNetCore.Mvc;
using Task.Models;
using Task.Repositories.IRepositories;

namespace Task.Controllers
{
    public class CourseController : Controller
    {
        private readonly IRepository<Course> _repo;
        public CourseController(IRepository<Course> repo)
        {
            _repo = repo;
        }
        public IActionResult Index()
        {
            var courses = _repo.GetAll();
            return View(courses);
        }
        public IActionResult Details(int id)
        {
            var course = _repo.GetById(id);

            if (course is null)
                return NotFound();

            return View(course);
        }
        public IActionResult Create() => View();
        [HttpPost]
        public IActionResult Create(Course course)
        {
            _repo.Add(course);
            _repo.Save();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var course = _repo.GetById(id);

            if (course is null)
                return NotFound();

            return View(course);
        }
        [HttpPost]
        public IActionResult Edit(Course course)
        {
            _repo.Update(course);
            _repo.Save();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            _repo.Delete(id);
            _repo.Save();
            return RedirectToAction("Index");
        }
    }
}
