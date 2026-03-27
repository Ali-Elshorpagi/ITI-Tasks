namespace Task03
{
    internal static class DatabaseLoader
    {
        private static readonly string _basePath = Path.Combine(AppContext.BaseDirectory, "../../../database");
        private static void EnsureDatabaseExists()
        {
            if (!Directory.Exists(_basePath))
                throw new DirectoryNotFoundException($"Database folder not found at: {_basePath}");
        }
        public static List<Student> LoadStudents()
        {
            EnsureDatabaseExists();

            List<Student> students = new();

            using StreamReader reader = new($"{_basePath}/students.txt");
            string? line;

            while ((line = reader.ReadLine()) is not null)
            {
                var parts = line.Split('|');
                students.Add(new Student(int.Parse(parts[0]), parts[1]));
            }
            return students;
        }
        public static List<Subject> LoadSubjects()
        {
            EnsureDatabaseExists();

            List<Subject> subjects = new();

            using StreamReader reader = new($"{_basePath}/subjects.txt");
            string? line;

            while ((line = reader.ReadLine()) is not null)
            {
                var parts = line.Split('|');
                subjects.Add(new Subject(int.Parse(parts[0]), parts[1]));
            }
            return subjects;
        }
        public static QuestionList LoadTrueFalseQuestions()
        {
            EnsureDatabaseExists();

            QuestionList questions = new($"{_basePath}/tf_log.txt");

            using StreamReader reader = new($"{_basePath}/questions_tf.txt");
            string? line;

            while ((line = reader.ReadLine()) is not null)
            {
                var parts = line.Split('|');
                questions.Add(new TrueFalseQuestion(parts[0], parts[1], int.Parse(parts[2]), bool.Parse(parts[3])));
            }
            return questions;
        }
        public static QuestionList LoadChooseOneQuestions()
        {
            EnsureDatabaseExists();

            QuestionList questions = new($"{_basePath}/one_log.txt");

            using StreamReader reader = new($"{_basePath}/questions_one.txt");
            string? line;

            while ((line = reader.ReadLine()) is not null)
            {
                var parts = line.Split('|');

                AnswerList answers = new();
                for (int i = 3; i < 6; ++i)
                    answers.Add(new Answer(parts[i], parts[i] == parts[6]));

                questions.Add(new ChooseOneQuestion(parts[0], parts[1], int.Parse(parts[2]), answers));
            }
            return questions;
        }
        public static QuestionList LoadChooseAllQuestions()
        {
            EnsureDatabaseExists();

            QuestionList questions = new($"{_basePath}/all_log.txt");

            using StreamReader reader = new($"{_basePath}/questions_all.txt");
            string? line;

            while ((line = reader.ReadLine()) is not null)
            {
                var parts = line.Split('|');
                var correctAnswers = parts[6].Split(',');

                AnswerList answers = new();
                for (int i = 3; i < 6; ++i)
                    answers.Add(new Answer(parts[i], correctAnswers.Contains(parts[i])));

                questions.Add(new ChooseAllQuestion(parts[0], parts[1], int.Parse(parts[2]), answers));
            }
            return questions;
        }
    }
}
