using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Task01
{
    public static class Helper
    {
        public static int ReadInt(string message)
        {
            int value;
            while (true)
            {
                Console.Write(message);
                if (int.TryParse(Console.ReadLine(), out value))
                    return value;

                Console.WriteLine("Invalid number, please try again.");
            }
        }
        public static decimal ReadDecimal(string message)
        {
            decimal value;
            while (true)
            {
                Console.Write(message);
                if (decimal.TryParse(Console.ReadLine(), out value) && value > -1)
                    return value;

                Console.WriteLine("Invalid decimal value, please try again.");
            }
        }
        public static Gender ReadGender()
        {
            while (true)
            {
                Console.Write("Enter Gender (M/F): ");
                string input = Console.ReadLine()?.Trim().ToUpper();

                if (Enum.TryParse(input, out Gender gender))
                    return gender;

                Console.WriteLine("Invalid gender, enter M or F only.");
            }
        }
        public static SecurityLevel ReadSecurityLevel()
        {
            Console.WriteLine("Available Security Levels:");
            Console.WriteLine("1 - Guest");
            Console.WriteLine("2 - Developer");
            Console.WriteLine("4 - Secretary");
            Console.WriteLine("8 - DBA");
            Console.WriteLine("You can combine values (e.g., 3 = Guest + Developer)");

            while (true)
            {
                Console.Write("Enter Security Level value: ");
                if (byte.TryParse(Console.ReadLine(), out byte value))
                    return (SecurityLevel)value;

                Console.WriteLine("Invalid security level value.");
            }
        }
        public static HireDate ReadHireDate()
        {
            string[] formats = { "d/M/yyyy", "dd/MM/yyyy" };

            while (true)
            {
                Console.Write("Enter Hire Date (dd/mm/yyyy): ");
                string input = Console.ReadLine();

                if (DateTime.TryParseExact(input, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
                    return new HireDate(date.Day, date.Month, date.Year);

                Console.WriteLine("Invalid date format");
            }
        }
        public static bool AskToExit()
        {
            while (true)
            {
                Console.WriteLine("\nDo you want to add another employee?");
                Console.WriteLine("1 - Yes");
                Console.WriteLine("2 - No (Exit)\n");
                Console.Write("Choice: ");

                string choice = Console.ReadLine()?.Trim();

                if (choice == "1")
                    return false;

                if (choice == "2")
                    return true;

                Console.WriteLine("Invalid choice. Please enter 1 or 2");
            }
        }
    }
}
