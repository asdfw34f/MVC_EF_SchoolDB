namespace SchoolTestsApp.ViewModels
{
    public class TestViewModelForCheck
    {
        TestViewModelToShow TestModel { get; set; }
        List<(int Question, string Answer)> List { get; set; } = new List<(int Question, string Answer)>();
    }
}
