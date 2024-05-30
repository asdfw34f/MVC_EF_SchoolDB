using SchoolTestsApp.Models.DB.Entities;

namespace SchoolTestsApp.ViewModels
{
    public class ChatViewModel
    {
        public List<History_Chat> Messages { get; set; }
        public int TeacherId { get; set; }
        public int StudentId { get; set; }
        public int ChatId { get; set; }
    }
}