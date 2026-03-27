namespace Task03
{
    internal class TrueFalseQuestion : Question
    {
        public override string QuestionType => "True / False";
        public TrueFalseQuestion(string header, string body, int marks, bool correctAnswer) : base(header, body, marks)
        {
            Answers.Add(new Answer("True", correctAnswer));
            Answers.Add(new Answer("False", !correctAnswer));
        }
        public override void Display()
        {
            Console.WriteLine(ToString());
            Console.WriteLine("1. True");
            Console.WriteLine("2. False");
        }
    }
}
