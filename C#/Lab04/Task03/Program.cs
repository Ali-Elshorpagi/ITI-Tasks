namespace Task03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Program...\n");

            List<Student> students = DatabaseLoader.LoadStudents();
            List<Subject> subjects = DatabaseLoader.LoadSubjects();
            QuestionList tfQuestions = DatabaseLoader.LoadTrueFalseQuestions();
            QuestionList oneQuestions = DatabaseLoader.LoadChooseOneQuestions();
            QuestionList allQuestions = DatabaseLoader.LoadChooseAllQuestions();

            //bool exit = false;
            //while (!exit)
            //{
            //    Console.Clear();
            //    Console.WriteLine("===== Examination System =====\n");
            //    Console.WriteLine("1 - Practice Exam");
            //    Console.WriteLine("2 - Final Exam");
            //    Console.WriteLine("3 - Exit");
            //    Console.Write("\nChoose: ");
            //    string choice = Console.ReadLine();
            //    switch (choice)
            //    {
            //        case "1":
            //            TestPracticeExam(students, subjects[0], tfQuestions, oneQuestions, allQuestions);
            //            Pause();
            //            break;
            //        case "2":
            //            TestFinalExam(students, subjects[1], tfQuestions, oneQuestions, allQuestions);
            //            Pause();
            //            break;
            //        case "3":
            //            exit = true;
            //            break;
            //        default:
            //            Console.WriteLine("Invalid choice!");
            //            break;
            //    }
            //}
            //TestPracticeExam(students, subjects[0], tfQuestions, oneQuestions, allQuestions);
            TestFinalExam(students, subjects[1], tfQuestions, oneQuestions, allQuestions);


            Console.WriteLine("\nEND OF PROGRAM");
        }
        static void TestPracticeExam(List<Student> students, Subject subject, QuestionList tf, QuestionList one, QuestionList all)     
        {
            Console.Clear();
            Console.WriteLine("========= PRACTICE EXAM =========\n");

            PracticeExam exam = new PracticeExam(60, subject);

            foreach (Student s in students)
                exam.ExamStarted += s.WhenExamStarted;

            AddQuestions(exam, tf);
            AddQuestions(exam, one);
            AddQuestions(exam, all);

            exam.NoQuestions = exam.QuestionAnswerDictionary.Count;
            exam.StartExam();

            //foreach (Student s in students)
            //    exam.ExamStarted -= s.WhenExamStarted;
        }
        static void TestFinalExam(List<Student> students, Subject subject, QuestionList tf, QuestionList one, QuestionList all)
        {
            Console.Clear();
            Console.WriteLine("========= FINAL EXAM =========\n");

            FinalExam exam = new FinalExam(60, subject);

            foreach (Student s in students)
                exam.ExamStarted += s.WhenExamStarted;

            AddQuestions(exam, tf);
            AddQuestions(exam, one);
            AddQuestions(exam, all);

            exam.NoQuestions = exam.QuestionAnswerDictionary.Count;
            exam.StartExam();

            //foreach (Student s in students)
            //    exam.ExamStarted -= s.WhenExamStarted;
        }
        static void AddQuestions(Exam exam, QuestionList source)
        {
            foreach (Question q in source)
            {
                AnswerList answers = new AnswerList();
                foreach (Answer a in q.Answers)
                    answers.Add(new Answer(a.Text, a.IsCorrect));

                Question newQuestion;

                if (q is TrueFalseQuestion)
                {
                    bool correct = q.Answers[0].IsCorrect;
                    newQuestion = new TrueFalseQuestion(q.Header, q.Body, q.Marks, correct);
                }
                else if (q is ChooseOneQuestion)
                    newQuestion = new ChooseOneQuestion(q.Header, q.Body, q.Marks, answers);
                else
                    newQuestion = new ChooseAllQuestion(q.Header, q.Body, q.Marks, answers);

                AnswerList correctAnswers = new AnswerList();
                foreach (Answer a in newQuestion.Answers)
                    if (a.IsCorrect)
                        correctAnswers.Add(a);

                exam.QuestionAnswerDictionary.Add(newQuestion, correctAnswers);
            }
        }
        static void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}
