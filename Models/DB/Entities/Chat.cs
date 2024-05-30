using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolTestsApp.Models.DB.Entities
{
    public class Chat
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int id { get; set; }
        public int Teacher_id { get; set; }
        public int Student_id { get; set; }
        public Teacher Teacher { get; set; }
        public Student Students { get; set; }
        public List<History_Chat> History_Chats { get; set; }
    }
}
