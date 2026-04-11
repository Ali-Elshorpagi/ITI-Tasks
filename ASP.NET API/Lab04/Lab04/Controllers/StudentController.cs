using Lab04.DTOs;
using Lab04.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab04.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "student,teacher")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentCrudRepository;
        public StudentController(IStudentRepository studentCrudRepository)
        {
            _studentCrudRepository = studentCrudRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var students = await _studentCrudRepository.GetAllAsync();
            return Ok(students);
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "teacher")]
        public async Task<ActionResult> GetById(string id)
        {
            var student = await _studentCrudRepository.GetByIdAsync(id);
            if (student is null)
                return NotFound();

            return Ok(student);
        }
    }
}
