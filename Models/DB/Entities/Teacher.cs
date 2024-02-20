using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolTestsApp.Models.DB.Entities
{
    public class Teacher
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int id { get; set; }
        public string? Name { get; set; }
        public string? SecondName { get; set; }
        public string? ThridName { get; set; }
        public DateOnly Birthday { get; set; }
        public string? Password { get; set; }
        public string? Login { get; set; }

        public List<Class> Classes { get; set; }
    }
}
