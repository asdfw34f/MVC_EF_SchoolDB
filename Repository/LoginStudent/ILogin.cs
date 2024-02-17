using SchoolTestsApp.Models.DB;
using SchoolTestsApp.Models.DB.Entities;

namespace SchoolTestsApp.Repository.LoginStudent
{
    public interface ILogin
    {
        Task<IEnumerable<Student>> getuser();
        Task<Student> AuthenticateStudent(string username, string password);
    }
}
