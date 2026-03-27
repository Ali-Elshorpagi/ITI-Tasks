namespace Task01
{
    internal class Ball
    {
        public int X { get; set; }
        public int Y { get; set; }
        public event EventHandler<Ball, BallEvent> BallPositionChanged;
        public Ball(int x, int y)
        {
            X = x;
            Y = y;
        }
        public void ChangePosition()
        {
            ++X; ++Y;
            BallPositionChanged?.Invoke(this, new BallEvent { DeltaX = X, DeltaY = Y });
        }
    }
}
