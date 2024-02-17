using SchoolTestsApp.Models.DB;
using SchoolTestsApp.Models.DB.Entities;

namespace SchoolTestsApp.Repository.LoginTeacher
{
    public interface ILogin
    {
        Task<IEnumerable<Teacher>> getuser();
        Task<Teacher> AuthenticateTeacher(string username, string password);
    }
}
