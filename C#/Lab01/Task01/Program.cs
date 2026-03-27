namespace Task01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine() ?? string.Empty;

            string []words = input.Split(' ');
            Array.Reverse(words);

            foreach (var word in words)
            {
                Console.Write($"{word} ");
            }
        }
    }
}
