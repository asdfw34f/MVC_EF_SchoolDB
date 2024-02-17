using System.ComponentModel.DataAnnotations;

namespace SchoolTestsApp.Models.DB
{
    public class Account
    {
        public required string login { get; set; }

        [DataType(DataType.Password)]
        public required string password { get; set; }

        public required bool isStident { get; set; }
    }
}
