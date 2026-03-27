namespace Task03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            NIC nic1 = NIC.GetOrCreateObj("Intel", "00-1A-2B-3C-4D-5E", NICType.Ethernet);

            NIC nic2 = NIC.GetOrCreateObj("Realtek", "AA-BB-CC-DD-EE-FF", NICType.TokenRing);

            Console.WriteLine(nic2);
            Console.WriteLine("----------------------------------");
            Console.WriteLine(nic1);
            Console.WriteLine();
            Console.WriteLine($"Are both NIC objects same? {ReferenceEquals(nic1, nic2)}");
        }
    }
}
