using SchoolTestsApp.Models.Serialize;

namespace SchoolTestsApp.ViewModels
{
    public class TestViewModelToShow
    {
        public TestModel Test { get; set; }
        public int idTest { get; set; }
        public int[] answers { get; set; }
    }
}