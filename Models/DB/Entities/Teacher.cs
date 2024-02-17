namespace SchoolTestsApp.Models.DB.Entities
{
    public class Teacher
    {
        public string? Name { get; set; }
        public string? SecondName { get; set; }
        public string? ThridName { get; set; }
        public DateOnly Birthday { get; set; }
        public int id { get; set; }
        public string? Password { get; set; }
        public string? Login { get; set; }

        public List<Class> Classes { get; set; }
    }
}
