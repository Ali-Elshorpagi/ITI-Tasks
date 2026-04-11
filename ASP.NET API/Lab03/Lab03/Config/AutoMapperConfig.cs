using AutoMapper;
using Lab03.DTOs;
using Lab03.Models;

namespace Lab03.Config
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Department, DepartmentDTO>().AfterMap(
                (src, dest) =>
                {
                    dest.StdCount = src.Students.Count();
                }
                );
            CreateMap<Student, StudentDTO>().AfterMap(
                (src, dest) =>
                {
                    dest.DeptName = src.Dept?.DeptName;
                    dest.StSuperName = src.StSuperNavigation?.StLname;
                }
                );
        }
    }
}
