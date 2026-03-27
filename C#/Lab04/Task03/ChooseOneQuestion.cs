namespace Task03
{
    internal class ChooseOneQuestion : Question
    {
        public override string QuestionType => "Choose One";
        public ChooseOneQuestion(string header, string body, int marks, AnswerList answers) : base(header, body, marks, answers)
        {
            if (answers.Count < 2)
                throw new ArgumentException("Choose One question must have at least two answers.");

            if (answers.Count(a => a.IsCorrect) != 1)
                throw new ArgumentException("Choose One question must have exactly one correct answer.");
        }
        public override void Display()
        {
            Console.WriteLine(ToString());
            for(int i = 0; i < Answers.Count; ++i)
                Console.WriteLine($"{i + 1}. {Answers[i].Text}");
        }
    }
}
