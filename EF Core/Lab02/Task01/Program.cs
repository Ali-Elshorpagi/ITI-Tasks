using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Task01.Domain.Models;
using Task01.Infrustructure.Data;

namespace Task01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Initializing
            using var context = new AppDbContext();

            var csDepartment = new Department
            {
                Name = "Computer Science",
                Description = "Department of Computer Science and Software Engineering"
            };

            var mathDepartment = new Department
            {
                Name = "Mathematics",
                Description = "Department of Pure and Applied Mathematics"
            };

            var student1 = new Student
            {
                Name = "Ahmed Ali",
                Age = 22,
                Department = csDepartment
            };

            var student2 = new Student
            {
                Name = "Sara Mohamed",
                Age = 21,
                Department = csDepartment
            };

            var student3 = new Student
            {
                Name = "Omar Hassan",
                Age = 23,
                Department = mathDepartment
            };

            var course1 = new Course
            {
                Name = "Data Structures",
                Duration = 45
            };

            var course2 = new Course
            {
                Name = "Database Systems",
                Duration = 60
            };

            var course3 = new Course
            {
                Name = "Calculus I",
                Duration = 50
            };

            var enrollment1 = new StudentCourse
            {
                Student = student1,
                Course = course1,
                Degree = 85
            };

            var enrollment2 = new StudentCourse
            {
                Student = student1,
                Course = course2,
                Degree = 92
            };

            var enrollment3 = new StudentCourse
            {
                Student = student2,
                Course = course1,
                Degree = 78
            };

            var enrollment4 = new StudentCourse
            {
                Student = student3,
                Course = course3,
                Degree = 95
            };
            #endregion

            #region Adding
            //context.Departments.AddRange(csDepartment, mathDepartment);
            //context.Students.AddRange(student1, student2, student3);
            //context.Courses.AddRange(course1, course2, course3);
            //context.StudentCourses.AddRange(enrollment1, enrollment2, enrollment3, enrollment4);

            //context.SaveChanges();
            //PrintDatabaseData(context);
            #endregion

            #region Updating
            //Console.WriteLine("Data before updates:");
            //PrintDatabaseData(context);

            //var studentToUpdate = context.Students.FirstOrDefault(s => s.Name == "Ahmed Ali");
            //if (studentToUpdate is not null)
            //{
            //    studentToUpdate.Age = 23;
            //    Console.WriteLine($"Updated {studentToUpdate.Name}'s age to {studentToUpdate.Age}");
            //}

            //var courseToUpdate = context.Courses.FirstOrDefault(c => c.Name == "Data Structures");
            //if (courseToUpdate is not null)
            //{
            //    courseToUpdate.Duration = 50;
            //    Console.WriteLine($"Updated {courseToUpdate.Name}'s duration to {courseToUpdate.Duration} hours");
            //}

            //var enrollmentToUpdate = context.StudentCourses
            //    .Include(sc => sc.Student)
            //    .Include(sc => sc.Course)
            //    .FirstOrDefault(sc => sc.Student.Name == "Sara Mohamed" && sc.Course.Name == "Data Structures");
            //if (enrollmentToUpdate is not null)
            //{
            //    enrollmentToUpdate.Degree = 88;
            //    Console.WriteLine($"Updated {enrollmentToUpdate.Student.Name}'s grade in {enrollmentToUpdate.Course.Name} to {enrollmentToUpdate.Degree}");
            //}

            //var deptToUpdate = context.Departments.FirstOrDefault(d => d.Name == "Mathematics");
            //if (deptToUpdate is not null)
            //{
            //    deptToUpdate.Description = "Department of Mathematics and Statistical Sciences";
            //    Console.WriteLine($"Updated {deptToUpdate.Name}'s description");
            //}

            //context.SaveChanges();
            //Console.WriteLine("\nData after updates:");
            //PrintDatabaseData(context);
            #endregion

            #region Deleting
            //Console.WriteLine("Data before deletions:");
            //PrintDatabaseData(context);

            //var enrollmentToDelete = context.StudentCourses
            //    .Include(sc => sc.Student)
            //    .Include(sc => sc.Course)
            //    .FirstOrDefault(sc => sc.Student.Name == "Ahmed Ali" && sc.Course.Name == "Database Systems");
            //if (enrollmentToDelete is not null)
            //{
            //    context.StudentCourses.Remove(enrollmentToDelete);
            //    Console.WriteLine($"Deleted enrollment: {enrollmentToDelete.Student.Name} from {enrollmentToDelete.Course.Name}");
            //}

            //var courseToDelete = context.Courses.FirstOrDefault(c => c.Name == "Calculus I");
            //if (courseToDelete is not null)
            //{
            //    context.Courses.Remove(courseToDelete);
            //    Console.WriteLine($"Deleted course: {courseToDelete.Name}");
            //}

            //var studentToDelete = context.Students.FirstOrDefault(s => s.Name == "Omar Hassan");
            //if (studentToDelete is not null)
            //{
            //    context.Students.Remove(studentToDelete);
            //    Console.WriteLine($"Deleted student: {studentToDelete.Name}");
            //}

            //context.SaveChanges();
            //Console.WriteLine("\nData after deletions:");
            //PrintDatabaseData(context);
            #endregion

            #region Loading Strategies
            // Eager Loading - Load related data with Include()
            var studentWithDepartment1 = context.Students.Where(x => x.Id == student1.Id).Include(d => d.Department).SingleOrDefault();
            Console.WriteLine(studentWithDepartment1?.Department?.Name);

            // Explicit Loading - Load related data explicitly when needed
            var studentWithDepartment2 = context.Students.Where(x => x.Id == student1.Id).SingleOrDefault();
            if (studentWithDepartment2 is not null)
            {
                context.Entry(studentWithDepartment2).Reference(x => x.Department).Load();
                Console.WriteLine(studentWithDepartment2?.Department?.Name);
            }

            // Lazy Loading - Related data loads automatically when accessed
            // Requires: virtual navigation properties, UseLazyLoadingProxies(), and Microsoft.EntityFrameworkCore.Proxies package
            var studentWithDepartment3 = context.Students.Where(x => x.Id == student1.Id).AsQueryable().SingleOrDefault();
            Console.WriteLine(studentWithDepartment3?.Department?.Name);
            #endregion

        }
        static void PrintDatabaseData(AppDbContext context)
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("=== DATABASE CONTENTS ===");
            Console.WriteLine("========================================\n");

            Console.WriteLine("=== Departments ===");
            var departments = context.Departments.Include(d => d.Students).ToList();
            foreach (var dept in departments)
            {
                Console.WriteLine($"Department: {dept.Name} - {dept.Description}");
                Console.WriteLine($"  Students Count: {dept.Students?.Count ?? 0}");
            }

            Console.WriteLine("\n=== Students ===");
            var students = context.Students.Include(s => s.Department).Include(s => s.StudentCourses).ToList();
            foreach (var student in students)
            {
                Console.WriteLine($"Student: {student.Name}, Age: {student.Age}");
                Console.WriteLine($"  Department: {student.Department?.Name ?? "N/A"}");
                Console.WriteLine($"  Enrolled Courses: {student.StudentCourses?.Count ?? 0}");
            }

            Console.WriteLine("\n=== Courses ===");
            var courses = context.Courses.Include(c => c.StudentCourses).ToList();
            foreach (var course in courses)
            {
                Console.WriteLine($"Course: {course.Name}, Duration: {course.Duration} hours");
                Console.WriteLine($"  Enrolled Students: {course.StudentCourses?.Count ?? 0}");
            }

            Console.WriteLine("\n=== Student Enrollments ===");
            var enrollments = context.StudentCourses
                .Include(sc => sc.Student)
                .Include(sc => sc.Course)
                .ToList();
            foreach (var enrollment in enrollments)
            {
                Console.WriteLine($"{enrollment.Student?.Name ?? "Unknown"} enrolled in {enrollment.Course?.Name ?? "Unknown"} - Grade: {enrollment.Degree}");
            }

            Console.WriteLine("\n========================================\n");
        }
    }
}
