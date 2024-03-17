namespace SchoolTestsApp.Models.Serialize
{
    [Serializable]
    public class TestModel
    {
        public string Title { get; set; } = "Title test";
        public List<QuestionModel> Questions { get; set; } = new List<QuestionModel>();
    }

    public class QuestionModel
    {
        public string Question { get; set; } = "Question text";
        public bool isMultiAnswer { get; set; } = false;
        public List<AnswerModel> Answers { get; set; } = new List<AnswerModel>();
    }

    public class AnswerModel
    {
        public string Answer { get; set; }
        public bool isRight { get; set; }
    }
}