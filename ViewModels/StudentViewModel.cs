using SchoolTestsApp.Models.DB.Entities;

namespace SchoolTestsApp.ViewModels
{
    public class StudentViewModel
    {
        public class ProfileViewModel
        {
            public Student Student { get; set; }
            public List<Test> PassedTests { get; set; }
            public List<Test> AvailableTests { get; set; }
        }
    }
}
