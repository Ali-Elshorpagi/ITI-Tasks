namespace Task03
{
    internal abstract class Question : ICloneable, IComparable<Question>
    {
        public string Header { get; set; }
        public string Body { get; set; }
        public int Marks { get; set; }
        public abstract string QuestionType { get; }
        public AnswerList Answers { get; set; }
        protected Question(string header, string body, int marks) : this(header, body, marks, new AnswerList()) { } 
        protected Question(string header, string body, int marks, AnswerList answers)
        {
            Header = header;
            Body = body;
            Marks = marks;
            Answers = answers;
        }
        public abstract void Display();
        public object Clone() => MemberwiseClone();
        public int CompareTo(Question? other) => other is null ? 1 : Marks.CompareTo(other.Marks);
        public override string ToString() => $"{Header}\n{Body}\nMarks: {Marks}";
        public override int GetHashCode() =>  HashCode.Combine(Header, Body);
        public override bool Equals(object obj) => obj is not Question other ? false : Header == other.Header && Body == other.Body;
    }
}
