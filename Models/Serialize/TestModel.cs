namespace SchoolTestsApp.Models.Serialize
{
    [Serializable]
    public class TestModel
    {
        public string Title { get; set; }
        public List<QuestionModel> Questions { get; set; }
    }

    [Serializable]
    public class QuestionModel
    {
        public int id { get; set; }
        public string Question { get; set; }

        public string Answer1 { get; set; } = "";
        public string Answer2 { get; set; } = "";
        public string Answer3 { get; set; } = "";
        public string Answer4 { get; set; } = "";

        public int RightAnswer { get; set; } = 0;
    }

}