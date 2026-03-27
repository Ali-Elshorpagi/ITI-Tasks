namespace Task01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Ball b1 = new Ball(4, 3);

            Player salah = new Player("Salah");
            Player marmoush = new Player("Marmoush");
            Referee r1 = new Referee();
            Audience audience = new Audience();

            b1.BallPositionChanged += salah.MovePlayer;
            b1.BallPositionChanged += marmoush.MovePlayer;
            b1.BallPositionChanged += r1.MoveReferee;
            b1.BallPositionChanged += audience.RaiseHand;

            b1.ChangePosition();
        }
    }
}
