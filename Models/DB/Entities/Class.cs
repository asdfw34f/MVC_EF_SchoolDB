namespace SchoolTestsApp.Models.DB.Entities
{
    public class Class
    {
        public int id { get; set; }
        public string? ClassCode { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set;}
        public List<Student> Students { get; set; }
    }
}
