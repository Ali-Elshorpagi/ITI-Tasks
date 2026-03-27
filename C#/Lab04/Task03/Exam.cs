using System.Reflection.PortableExecutable;

namespace Task03
{
    internal abstract class Exam : ICloneable, IComparable<Exam>
    {
        public int Time { get; set; }
        public int NoQuestions { get; set; }
        public ExamMode Mode { get; set; }
        public Subject Subject { get; set; }
        public Dictionary<Question, AnswerList> QuestionAnswerDictionary { get; set; }
        public event ExamStartedHandler ExamStarted;
        protected Exam()
        {
            QuestionAnswerDictionary = new Dictionary<Question, AnswerList>();
            Mode = ExamMode.Queued;
        }
        protected Exam(int time, Subject subject) : this()
        {
            Time = time;
            Subject = subject;
        }
        public void StartExam()
        {
            Mode = ExamMode.Starting;
            WhenExamStarted();
            ShowExam();
            Mode = ExamMode.Finished;
        }
        protected virtual void WhenExamStarted() => ExamStarted?.Invoke($"Exam for {Subject.Name} has started");
        public abstract void ShowExam();
        public object Clone() => MemberwiseClone();
        public int CompareTo(Exam other) => Time.CompareTo(other.Time);
        public override int GetHashCode() => HashCode.Combine(Subject.Name, Time);
        public override string ToString() => $"{Subject.Name} Exam | Time: {Time} mins | Questions: {NoQuestions}";
        public override bool Equals(object obj) => obj is not Exam exam ? false : Subject.Name == exam.Subject.Name && Time == exam.Time;
    }
}
