using AccountLibrary.Model;
using SchoolTestsApp.Models.DB.Entities;

namespace SchoolTestsApp.Repository.Authentication
{
    public interface ILogin
    {
        protected Task<Student> AuthenticateStudent(string username, string password);
        protected Task<Teacher> AuthenticateTeacher(string username, string password);

        public Task<bool> Authectication(string username, string password, HttpContext context);

    }
}