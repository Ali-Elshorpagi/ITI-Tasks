namespace Task05
{
    internal class EvenSeries : ISeries
    {
        private int _nextNumber = 0;
        public int GetNextNumber()
        {
            int current = _nextNumber;
            _nextNumber += 2;
            return current;
        }
    }
}
