using Microsoft.AspNetCore.Mvc;
using Task.Models;
using Task.Repositories.IRepositories;

namespace Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        public CourseController(ICourseRepository courseRepository) => _courseRepository = courseRepository;
        // GET: api/course
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var courses = await _courseRepository.GetAllAsync();

            if (courses is null || !courses.Any())
                return NotFound();

            return Ok(courses);
        }

        // GET: api/course/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var course = await _courseRepository.GetByIdAsync(id);

            if (course is null)
                return NotFound();

            return Ok(course);
        }

        // GET: api/course/byName/{name}
        [HttpGet("byName/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var course = await _courseRepository.GetCourseByNameAsync(name);

            if (course is null)
                return NotFound();

            return Ok(course);
        }
        // POST: api/course
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Course course)
        {
            if (course is null)
                return BadRequest();

            var createdCourse = await _courseRepository.AddAsync(course);

            return CreatedAtAction(nameof(GetById), new { id = createdCourse.Id }, createdCourse);
        }

        // PUT: api/course/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Course course)
        {
            if (course is null || id != course.Id)
                return BadRequest();

            var existing = await _courseRepository.GetByIdAsync(id);
            if (existing is null)
                return NotFound();

            await _courseRepository.UpdateAsync(id, course);

            return NoContent(); // 204
        }

        // DELETE: api/course/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(string id)
        {
            var existing = await _courseRepository.GetByIdAsync(id);
            if (existing is null)
                return NotFound();

            await _courseRepository.DeleteCourseByIdAsync(id);

            var courses = await _courseRepository.GetAllAsync();

            return Ok(courses);
        }
    }
}
