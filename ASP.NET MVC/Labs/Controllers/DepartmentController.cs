using Microsoft.AspNetCore.Mvc;
using Task.Models;
using Task.Repositories.IRepositories;
using Task.ViewModels;

namespace Task.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IRepository<Department> _repo;
        private readonly IRepository<Course> _courseRepo;
        public DepartmentController(IRepository<Department> repo, IRepository<Course> courseRepo)
        {
            _repo = repo;
            _courseRepo = courseRepo;
        }
        public IActionResult Index()
        {
            var departments = _repo.GetAll();
            return View(departments);
        }
        public IActionResult Details(int id)
        {
            var department = _repo.GetById(id);

            if (department is null)
                return NotFound();

            return View(department);
        }
        public IActionResult Create() => View();
        [HttpPost]
        public IActionResult Create(Department dept)
        {
            if (ModelState.IsValid)
            {
                _repo.Add(dept);
                _repo.Save();
                return RedirectToAction("Index");
            }
            return View(dept);
        }
        public IActionResult Edit(int id)
        {
            var dept = _repo.GetById(id);
            if (dept is null)
                return NotFound();

            return View(dept);
        }
        [HttpPost]
        public IActionResult Edit(Department dept)
        {
            if (ModelState.IsValid)
            {
                _repo.Update(dept);
                _repo.Save();
                return RedirectToAction("Index");
            }
            return View(dept);
        }
        public IActionResult Delete(int id)
        {
            _repo.Delete(id);
            _repo.Save();
            return RedirectToAction("Index");
        }
        public IActionResult ManageCourses(int id)
        {
            var department = _repo.GetById(id);

            if (department is null)
                return NotFound();

            var allCourses = _courseRepo.GetAll();
            var assignedIds = department.Courses.Select(c => c.Id).ToHashSet();

            var vm = new DepartmentCoursesViewModel
            {
                Department = department,
                AssignedCourses = department.Courses.ToList(),
                AvailableCourses = allCourses.Where(c => !assignedIds.Contains(c.Id)).ToList()
            };

            return View(vm);
        }
        [HttpPost]
        public IActionResult AddCourse(int departmentId, int courseId)
        {
            var department = _repo.GetById(departmentId);
            var course = _courseRepo.GetById(courseId);

            if (department is null || course is null)
                return NotFound();

            department.Courses.Add(course);
            _repo.Save();

            return RedirectToAction("ManageCourses", new { id = departmentId });
        }
        [HttpPost]
        public IActionResult RemoveCourse(int departmentId, int courseId)
        {
            var department = _repo.GetById(departmentId);

            if (department is null)
                return NotFound();

            var course = department.Courses.FirstOrDefault(c => c.Id == courseId);

            if (course is null)
                return NotFound();

            department.Courses.Remove(course);
            _repo.Save();

            return RedirectToAction("ManageCourses", new { id = departmentId });
        }
    }
}
