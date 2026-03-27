namespace Task05
{
    internal class PowerdSeries : ISeries
    {
        private int _nextNumber = 1;
        public int GetNextNumber()
        {
            int current = _nextNumber;
            _nextNumber *= 2;
            return current;
        }
    }
}
