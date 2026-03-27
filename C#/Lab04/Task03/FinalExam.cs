using Task03;

namespace Task03
{
    internal class FinalExam : Exam
    {
        public FinalExam(int time, Subject subject) : base(time, subject) { }

        public override void ShowExam()
        {
            int qNo = 0;

            foreach (var item in QuestionAnswerDictionary)
            {
                Question question = item.Key;

                Console.WriteLine($"Question {++qNo}");
                Console.WriteLine($"Type: {question.QuestionType}");
                question.Display();

                Console.WriteLine(new string('-', 45));
            }

            Console.WriteLine("Correct answers are hidden in Final Exam");
        }
    }
}