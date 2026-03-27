using System;
using System.Collections.Generic;
using System.Text;

namespace Task02
{
    internal static class MathS
    {
        public static int Add(int a, int b) => a + b;
        public static int Subtract(int a, int b) => a - b;
        public static int Multiply(int a, int b) => a * b;
        public static double Divide(int a, int b)
        {
            if (b == 0)
            {
                Console.WriteLine("Cannot divide by zero.");
                return 0;
            }
            return (double)a / b;
        }
    }
}
