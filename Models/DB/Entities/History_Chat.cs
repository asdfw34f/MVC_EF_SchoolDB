using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolTestsApp.Models.DB.Entities
{
    public class History_Chat
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int id { get; set; }
        public int Chat_id { get; set; }
        public string Message { get; set; }
        public bool isTeacher { get; set; }
        public DateTime atDate { get; set; }
        public Chat Chats { get; set; }

    }
}
