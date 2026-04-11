using AutoMapper;
using Task01.DTOs.Department;
using Task01.DTOs.Student;
using Task01.Models;

namespace Task01.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Department, DepartmentReadDto>()
            .ForMember(dest => dest.StudentCount, opt => opt.MapFrom(src => src.Students.Count));

        CreateMap<DepartmentUpsertDto, Department>();

        CreateMap<Student, StudentReadDto>()
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : string.Empty))
            .ForMember(dest => dest.SupervisorName, opt => opt.MapFrom(src => src.Supervisor != null ?
                string.Join(' ', new[] { src.Supervisor.FirstName, src.Supervisor.LastName }.Where(v => !string.IsNullOrWhiteSpace(v))) : null));

        CreateMap<StudentUpsertDto, Student>();
    }
}

