namespace SchoolTestsApp.Models.DB.TestJson
{
    public class TestModel
    {
        public string question { get; set; }
        public IEnumerable<Answer> answers { get; set; }
    }
}
