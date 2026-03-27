namespace Task02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MathObj();
            Console.WriteLine("-----------------------------");
            MathST();
        }
        static void MathObj()
        {
            Math math = new Math();

            Console.WriteLine($"Add: {math.Add(10, 5)}");
            Console.WriteLine($"Subtract: {math.Subtract(10, 5)}");
            Console.WriteLine($"Multiply: {math.Multiply(10, 5)}");
            Console.WriteLine($"Divide: {math.Divide(10, 5)}");
        }
        static void MathST()
        {
            Console.WriteLine($"Add: {MathS.Add(20, 4)}");
            Console.WriteLine($"Subtract: {MathS.Subtract(20, 4)}");
            Console.WriteLine($"Multiply: {MathS.Multiply(20, 4)}");
            Console.WriteLine($"Divide: {MathS.Divide(20, 4)}");
        }
    }
}
