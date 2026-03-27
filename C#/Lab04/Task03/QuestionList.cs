namespace Task03
{
    internal class QuestionList : List<Question>
    {
        private readonly string _filePath;
        public QuestionList(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path cannot be empty.");

            _filePath = filePath;
        }
        public new void Add(Question question)
        {
            if (question is null)
                throw new ArgumentNullException("Question can not be null");

            base.Add(question);

            try
            {
                using StreamWriter writer = new StreamWriter(_filePath, true);
                writer.WriteLine(question);
                writer.WriteLine("----------------------");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"File logging error: {ex.Message}");
            }
        }
    }
}
