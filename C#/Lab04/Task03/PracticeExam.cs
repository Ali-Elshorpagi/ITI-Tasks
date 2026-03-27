using Task03;

namespace Task03
{
    internal class PracticeExam : Exam
    {
        public PracticeExam(int time, Subject subject) : base(time, subject) { }
        public override void ShowExam()
        {
            Console.WriteLine($"Practice Exam Started");
            int qNo = 0;
            foreach (var item in QuestionAnswerDictionary)
            {
                Question question = item.Key;
                AnswerList correctAnswers = item.Value;

                Console.WriteLine($"Question {++qNo}");
                Console.WriteLine($"Type: {question.QuestionType}");
                question.Display();

                Console.WriteLine("Correct Answer(s):");
                foreach (var ans in correctAnswers)
                    Console.WriteLine($"{ans.Text}");

                Console.WriteLine(new string('-', 45));
            }
        }
    }
}