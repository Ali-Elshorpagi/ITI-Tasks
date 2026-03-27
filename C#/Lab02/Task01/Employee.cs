using System;
using System.Collections.Generic;
using System.Text;

namespace Task01
{
    /// <summary>
    /// Represents an employee in the company
    /// </summary>
    public struct Employee
    {       
        /// <summary>
        /// Unique identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Security privileges
        /// </summary>
        public SecurityLevel SecurityLevel { get; set; }

        /// <summary>
        /// Employee salary
        /// </summary>
        public decimal Salary { get; set; }

        /// <summary>
        /// Employee hire date
        /// </summary>
        public HireDate HireDate { get; set; }

        /// <summary>
        /// Employee gender
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// Initializes a new instance of the Employee structure
        /// </summary>
        /// <param name="id">Employee ID</param>
        /// <param name="securityLevel">Employee security privileges</param>
        /// <param name="salary">Employee salary</param>
        /// <param name="gender">Employee gender</param>
        /// <param name="hireDate">Employee hire date</param>
        public Employee(int id, SecurityLevel securityLevel, decimal salary, Gender gender, HireDate hireDate)
        {
            Id = id;
            SecurityLevel = securityLevel;
            Salary = salary;
            Gender = gender;
            HireDate = hireDate;
        }
        /// <summary>
        /// Returns a string representation of the employee data
        /// </summary>
        /// <returns>
        /// A formatted string containing employee details
        /// </returns>
        public override string ToString()
        {
            return $"ID: {Id}, " +
                    $"Security Level: {SecurityLevel}, " +
                    $"Salary: {Salary:C}, " +
                    $"Gender: {Gender}, " +
                    $"Hire Date: {HireDate}";
        }
    }
}
