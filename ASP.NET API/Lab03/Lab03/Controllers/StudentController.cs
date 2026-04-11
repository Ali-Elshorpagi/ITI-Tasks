using AutoMapper;
using Lab03.DTOs;
using Lab03.Models;
using Lab03.UoW;
using Microsoft.AspNetCore.Mvc;

namespace Lab03.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly UnitOFWork unit;
        private readonly IMapper map;
        public StudentController(UnitOFWork unit,IMapper map)
        {
            this.unit = unit;
            this.map = map;
        }
        [HttpGet("GetAll")]
        public IActionResult Get()
        {
            var std = unit.StudReps.StudentsWithSuperAndDept();
            return Ok(map.Map<List<StudentDTO>>(std));
        }
        [HttpGet("Search")]
        public IActionResult Get([FromQuery] string? search, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if (pageNumber < 1)
                pageNumber = 1;

            if (pageSize < 1)
                pageSize = 10;

            if (pageSize > 100)
                pageSize = 100;

            var query = unit.StudReps.StudentsWithSuperAndDept();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(s =>
                    s.StFname.Contains(search) ||
                    s.StLname.Contains(search) ||
                    (s.Dept != null && s.Dept.DeptName.Contains(search)));
            }

            var totalCount = query.Count();
            var std = query
                .OrderBy(s => s.StId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Ok(new
            {
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Data = map.Map<List<StudentDTO>>(std)
            });
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var std = unit.StudReps.StudentsWithSuperAndDept().FirstOrDefault(a => a.StId == id);
            if (std is null)
                return NotFound();

            return Ok(map.Map<StudentDTO>(std));
        }
        [HttpPost]
        public IActionResult Post(Student student)
        {
            if (!unit.StudReps.CanUseId(student.StId))
                return Conflict("Student with the same id already exists.");

            unit.StudReps.Add(student);
            unit.save();

            var created = unit.StudReps.StudentsWithSuperAndDept()
                .First(s => s.StId == student.StId);

            return CreatedAtAction(nameof(GetById), new { id = student.StId }, map.Map<StudentDTO>(created));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Student student)
        {
            if (id != student.StId)
                return BadRequest();
            
            var existing = unit.StudReps.FindById(id);
            if (existing is null)
                return NotFound();

            unit.StudReps.Edit(student);
            unit.save();

            var updated = unit.StudReps.StudentsWithSuperAndDept()
                .First(s => s.StId == id);

            return Ok(map.Map<StudentDTO>(updated));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var res = unit.StudReps.delete(id);
            if (res == true)
            {
                unit.save();
                return NoContent();
            }
            return NotFound();
        }
    }
}
