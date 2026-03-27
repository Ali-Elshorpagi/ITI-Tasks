namespace Task01
{
    internal class Player
    {
        public string Name { get; set; }
        public Player(string name) => Name = name;
        public void MovePlayer(Ball ball, BallEvent ballEvent) => Console.WriteLine($"{Name} moves by {ballEvent.DeltaX}, {ballEvent.DeltaY}");
    }
}
