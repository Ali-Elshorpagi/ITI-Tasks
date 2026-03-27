namespace Task03
{
    internal class Answer
    {
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
        public Answer(string text, bool isCorrect)
        {
            this.Text = text;
            this.IsCorrect = isCorrect;
        }
        public override string ToString() => $"{Text} (Correct: {IsCorrect})";
    }
}
