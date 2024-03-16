namespace SchoolTestsApp.Models
{
    public class QuestionModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public List<AnswerModel> Answers { get; set; } = new List<AnswerModel>() { new AnswerModel()};
    }
    public class AnswerModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        bool isRight { get; set; } = false;
    }
}
