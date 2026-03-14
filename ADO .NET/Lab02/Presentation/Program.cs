using BLL;

namespace Presentation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("========== ShowAll() ==========");
            ShowAll();

            Console.WriteLine("\n========== Insert() ==========");
            Department newDept = new Department
            {
                Name = "Cloud",
                Capacity = 120
            };

            if (DepartmentManager.AddNewDept(newDept))
                Console.WriteLine("Department Added Successfully!");

            ShowAll();

            Console.WriteLine("\n========== GetDeptById(1) ==========");
            var dept = DepartmentManager.GetDeptById(1);
            if (dept is not null)
                Console.WriteLine(dept);
            else
                Console.WriteLine("Department not found");

            Console.WriteLine("\n========== Update ==========");
            if (dept is not null)
            {
                dept.Name = "Cloud Updated";
                dept.Capacity = 200;

                if (DepartmentManager.UpdateDept(dept))
                    Console.WriteLine("Department Updated Successfully!");
            }

            ShowAll();

            Console.WriteLine("\n========== Delete ==========");
            Console.Write("Enter ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int deleteId))
            {
                if (DepartmentManager.DeleteDept(deleteId))
                    Console.WriteLine("Department Deleted Successfully!");
                else
                    Console.WriteLine("Delete Failed!");
            }

            ShowAll();

        }

        static void ShowAll()
        {
            var res = DepartmentManager.GetAllDepartments();
            foreach (var item in res)
                Console.WriteLine(item);
        }
    }
}
