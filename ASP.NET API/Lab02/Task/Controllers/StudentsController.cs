using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Task01.DTOs.Common;
using Task01.DTOs.Student;
using Task01.Models;
using Task01.Repositories.Interfaces;

namespace Task01.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IStudentRepository _studentRepository;
    private readonly IMapper _mapper;
    public StudentsController(IStudentRepository studentRepository, IMapper mapper)
    {
        _studentRepository = studentRepository;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<ActionResult<PagedResultDto<StudentReadDto>>> GetAll([FromQuery] string? search, [FromQuery] int pageNumber = 1,[FromQuery] int pageSize = 10)
    {
        pageNumber = pageNumber < 1 ? 1 : pageNumber;
        pageSize = pageSize is < 1 or > 100 ? 10 : pageSize;

        var (students, totalCount) = await _studentRepository.GetPagedAsync(search, pageNumber, pageSize);

        var result = new PagedResultDto<StudentReadDto>
        {
            Items = _mapper.Map<ICollection<StudentReadDto>>(students),
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount
        };

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<StudentReadDto>> GetById(int id)
    {
        var student = await _studentRepository.GetByIdAsync(id);

        if (student is null)
            return NotFound();

        return Ok(_mapper.Map<StudentReadDto>(student));
    }

    [HttpPost]
    public async Task<ActionResult<StudentReadDto>> Create(StudentUpsertDto dto)
    {
        if (!await _studentRepository.DepartmentExistsAsync(dto.DepartmentId))
            return BadRequest("Department does not exist.");

        if (dto.SupervisorId.HasValue && !await _studentRepository.ExistsAsync(dto.SupervisorId.Value))
            return BadRequest("Supervisor does not exist.");

        var student = _mapper.Map<Student>(dto);
        await _studentRepository.AddAsync(student);

        var created = await _studentRepository.GetByIdAsync(student.Id);

        return CreatedAtAction(nameof(GetById), new { id = student.Id }, _mapper.Map<StudentReadDto>(created));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, StudentUpsertDto dto)
    {
        var student = await _studentRepository.GetByIdAsync(id);
        if (student is null)
            return NotFound();

        if (!await _studentRepository.DepartmentExistsAsync(dto.DepartmentId))
            return BadRequest("Department does not exist.");

        if (dto.SupervisorId == id)
            return BadRequest("Student cannot supervise self.");

        if (dto.SupervisorId.HasValue && !await _studentRepository.ExistsAsync(dto.SupervisorId.Value))
            return BadRequest("Supervisor does not exist.");

        _mapper.Map(dto, student);
        await _studentRepository.UpdateAsync(student);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var student = await _studentRepository.GetByIdAsync(id);
        if (student is null)
            return NotFound();

        await _studentRepository.DeleteAsync(student);

        return NoContent();
    }
}

