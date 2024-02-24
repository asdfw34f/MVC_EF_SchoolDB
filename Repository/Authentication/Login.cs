using SchoolTestsApp.Models.DB.Entities;
using SchoolTestsApp.Models.DB;
using AccountLibrary.Model;
using Microsoft.EntityFrameworkCore;
using AccountLibrary.Serviece;

namespace SchoolTestsApp.Repository.Authentication
{
    public class Login : ILogin
    {
        private readonly ApplicationContext context;

        public Login(ApplicationContext context)
        {
            this.context = context;
        }
        
        public async Task<Student> AuthenticateStudent(string username, string password)
        {
            var succeeded = await context.Students.FirstOrDefaultAsync(u => u.Login == username && u.Password == password);
            return succeeded;
        }
       
        public async Task<Teacher> AuthenticateTeacher(string username, string password)
        {
            var succeeded = await context.Teachers.FirstOrDefaultAsync(u => u.Login == username && u.Password == password);
            return succeeded;
        }

      
        public async Task<bool> Authectication(string username, string password)
        {
            var s = AuthenticateStudent(username, password);
            if (s.Result != null)
            {
                Manager.Init(
                                   s.Result.Name,
                                   s.Result.SecondName,
                                   s.Result.id,
                                   true);
                return true;
            }
            else 
            {
                var t = AuthenticateTeacher(username, password);
                if (t.Result != null)
                {
                    Manager.Init(
                                   t.Result.Name,
                                   t.Result.SecondName,
                                   t.Result.id,
                                   false);
                    return true;
                }
            }
            return false;
        }
    }
}