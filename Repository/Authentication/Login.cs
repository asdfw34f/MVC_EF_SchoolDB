using SchoolTestsApp.Models.DB.Entities;
using SchoolTestsApp.Models.DB;
using AccountLibrary.Model;
using Microsoft.EntityFrameworkCore;
using AccountLibrary.Serviece;
using SchoolTestsApp.AuthenticationModule.LoginCookie;
namespace SchoolTestsApp.Repository.Authentication
{
    public class Login : ILogin
    {
        private readonly ApplicationContext context;
        private readonly IAuthentication _authentication;

        public Login(ApplicationContext context)
        {
            this.context = context;
            _authentication = new AuthenticationModule.LoginCookie.Authentication();
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

      
        public async Task<bool> Authectication(string username, string password, HttpContext httpContext)
        {
            var s = AuthenticateStudent(username, password);
            if (s.Result != null)
            {
                Manager.Init(
                                   s.Result.Login,
                                   s.Result.Password,
                                   s.Result.id,
                                   true);

                await _authentication.Log_In(username, password, httpContext);

                return true;
            }
            else 
            {
                var t = AuthenticateTeacher(username, password);
                if (t.Result != null)
                {
                    Manager.Init(
                                   t.Result.Login,
                                   t.Result.Password,
                                   t.Result.id,
                                   false);
                    await _authentication.Log_In(username, password, httpContext);
                    return true;
                }
            }
            return false;
        }
    }
}