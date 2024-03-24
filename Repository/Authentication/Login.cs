using SchoolTestsApp.Models.DB.Entities;
using SchoolTestsApp.Models.DB;
using Microsoft.EntityFrameworkCore;
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
            var sts = await context.Students.ToListAsync();
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
            var s = await AuthenticateStudent(username, password);
            if (s != null)
            {
                AuthenticationModule.Account.init(
                    s.id,
                    s.Login,
                    s.Password,
                    true);

                await _authentication.Log_In(username, password, httpContext);

                return true;
            }
            else
            {
                var t = await AuthenticateTeacher(username, password);
                if (t != null)
                {
                    AuthenticationModule.Account.init(
                    t.id,
                    t.Login,
                    t.Password,
                    false);
                    await _authentication.Log_In(username, password, httpContext);
                    return true;
                }
            }
            return false;
        }
    }
}