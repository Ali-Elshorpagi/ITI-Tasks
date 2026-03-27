namespace Task05
{
    internal static class SeriesPrinter
    {
        public static void PrintNext(ISeries series)
        {
            Console.WriteLine(series.GetNextNumber());
        }
    }
}
