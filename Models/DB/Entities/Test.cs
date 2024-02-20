using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolTestsApp.Models.DB.Entities
{
    public class Test
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int id { get; set; }
        public string? Title { get; set; }
        public byte[] TestFile { get; set; }
        public int Class { get; set; }

        public List<HistoryTests> HistoryTests { get; set; }
    }
}
