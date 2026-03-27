namespace Task03
{
    internal class AnswerList : List<Answer>
    {
        public IEnumerable<Answer> GetCorrectAnswers() => this.Where(a => a.IsCorrect).ToList();
    }
}
