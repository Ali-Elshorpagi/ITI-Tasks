using Microsoft.AspNetCore.Mvc;
using Task.Models;
using Task.Repositories;
using Task.Repositories.IRepositories;
using Task.ViewModels;

namespace Task.Controllers
{
    public class StudentController : Controller
    {
        private readonly IRepository<Student> _stdRepo;
        private readonly IRepository<Department> _deptRepo;
        public StudentController(IRepository<Student> stdRepo, IRepository<Department> deptRepo)
        {
            _stdRepo = stdRepo;
            _deptRepo = deptRepo;
        }
        public IActionResult Index()
        {
            var students = _stdRepo.GetAll();
            return View(students);
        }
        public IActionResult Details(int id)
        {
            var student = _stdRepo.GetById(id);

            if (student is null)
                return NotFound();

            return View(student);
        }
        public IActionResult Create()
        {
            ViewBag.Depts = _deptRepo.GetAll();
            return View();
        }
        [HttpPost]
        public IActionResult Create(StudentViewModel stdVm)
        {
            if (ModelState.IsValid)
            {
                Student newStudent = new Student
                {
                    Name = stdVm.Name,
                    Age = stdVm.Age,
                    Email = stdVm.Email,
                    DepartmentId = stdVm.DepartmentId.Value
                };

                _stdRepo.Add(newStudent);
                _stdRepo.Save();
                return RedirectToAction("Index");
            }

            ViewBag.Depts = _deptRepo.GetAll();
            return View(stdVm);
        }
        public IActionResult Edit(int id)
        {
            var student = _stdRepo.GetById(id);
            if (student is null) 
                return NotFound();

            var stdVm = new StudentViewModel
            {
                Id = student.Id,
                Name = student.Name,
                Age = student.Age,
                Email = student.Email,
                DepartmentId = student.DepartmentId
            };
            ViewBag.Depts = _deptRepo.GetAll();
            return View(stdVm);
        }
        [HttpPost]
        public IActionResult Edit(StudentViewModel stdVm)
        {
            if (ModelState.IsValid)
            {
                Student existingStudent = new Student
                {
                    Id = stdVm.Id,
                    Name = stdVm.Name,
                    Age = stdVm.Age,
                    Email = stdVm.Email,
                    DepartmentId = stdVm.DepartmentId.Value
                };
                _stdRepo.Update(existingStudent);
                _stdRepo.Save();
                return RedirectToAction("Index");
            }
            ViewBag.Depts = _deptRepo.GetAll();
            return View(stdVm);
        }
        public IActionResult Delete(int id)
        {
            _stdRepo.Delete(id);
            _stdRepo.Save();
            return RedirectToAction("Index");
        }
    }
}
