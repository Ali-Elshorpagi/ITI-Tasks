namespace Task03
{
    internal class ChooseAllQuestion : Question
    {
        public override string QuestionType => "Choose All";
        public ChooseAllQuestion(string header, string body, int marks, AnswerList answers) : base(header, body, marks, answers)
        {
            if (answers.Count < 2)
                throw new ArgumentException("Choose All question must have at least two answers.");

            if (answers.Count(a => a.IsCorrect) < 2)
                throw new ArgumentException("Choose All question must have more than one correct answer.");
        }
        public override void Display()
        {
            Console.WriteLine(ToString());
            for(int i = 0; i < Answers.Count; ++i)
                Console.WriteLine($"{i + 1}. {Answers[i].Text}");
        }
    }
}
