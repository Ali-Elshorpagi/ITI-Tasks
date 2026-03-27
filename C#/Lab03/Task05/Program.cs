namespace Task05
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ISeries odd = new OddSeries();
            ISeries even = new EvenSeries();
            ISeries power = new PowerdSeries();

            SeriesPrinter.PrintNext(odd);  
            SeriesPrinter.PrintNext(odd);
            Console.WriteLine("--------------------------");
            SeriesPrinter.PrintNext(even); 
            SeriesPrinter.PrintNext(even);
            Console.WriteLine("--------------------------");
            SeriesPrinter.PrintNext(power);
            SeriesPrinter.PrintNext(power);
            SeriesPrinter.PrintNext(power);
        }
    }
}
