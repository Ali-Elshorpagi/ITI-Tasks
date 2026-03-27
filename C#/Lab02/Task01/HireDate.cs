using System;
using System.Collections.Generic;
using System.Text;

namespace Task01
{
    /// <summary>
    /// Represents the hiring date of an employee
    /// </summary>
    public struct HireDate : IComparable
    {
        /// <summary>Day of hiring</summary>
        public int Day { get; set; }

        /// <summary>Month of hiring</summary>
        public int Month { get; set; }

        /// <summary>Year of hiring</summary>
        public int Year { get; set; }

        /// <summary>
        /// Initializes a new instance of the HireDate structure
        /// </summary>
        /// <param name="day">Hiring day</param>
        /// <param name="month">Hiring month</param>
        /// <param name="year">Hiring year</param>
        public HireDate(int day, int month, int year)
        {
            Day = day;
            Month = month;
            Year = year;
        }
        /// <summary>
        /// Returns the hire date in dd/mm/yyyy format
        /// </summary>
        /// <returns>A formatted hire date string</returns>
        public override string ToString()
        {
            return $"{Day:D2}/{Month:D2}/{Year}";
        }
        /// <summary>
        /// Compares this hire date with another hire date
        /// </summary>
        /// <param name="obj">The HireDate object to compare with</param>
        /// <returns>
        /// A value less than zero if this date is earlier, zero if equal, greater than zero if later
        /// </returns>
        public int CompareTo(object? obj)
        {

            HireDate other = (HireDate)obj;

            if (Year != other.Year) 
                return Year.CompareTo(other.Year);

            if (Month != other.Month) 
                Month.CompareTo(other.Month);

            return Day.CompareTo(other.Day);
        }
    }
}
