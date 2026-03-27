namespace Task01
{
    public class Program
    {
        static void Main(string[] args)
        {

            List<Employee> employees = new List<Employee>();
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("==================================");
                Console.WriteLine("      Employee Entry System       ");
                Console.WriteLine("==================================\n");

                int id = Helper.ReadInt("Enter ID: ");
                SecurityLevel security = Helper.ReadSecurityLevel();
                decimal salary = Helper.ReadDecimal("Enter Salary: ");
                Gender gender = Helper.ReadGender();
                HireDate hireDate = Helper.ReadHireDate();

                employees.Add(new Employee(id, security, salary, gender, hireDate));

                Console.WriteLine("\nEmployee added successfully \n");

                exit = Helper.AskToExit();
            }

            Employee[] empArr = employees.ToArray();

            Array.Sort(empArr, (e1, e2) => e1.HireDate.CompareTo(e2.HireDate));

            Console.Clear();
            Console.WriteLine("==================================");
            Console.WriteLine("   Employees Sorted By Hire Date  ");
            Console.WriteLine("==================================\n");

            foreach (Employee emp in empArr)
            {
                Console.WriteLine(emp);
                Console.WriteLine("---------------------------------------------------------");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
