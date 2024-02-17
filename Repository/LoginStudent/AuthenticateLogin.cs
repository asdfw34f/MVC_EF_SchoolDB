using Microsoft.EntityFrameworkCore;
using SchoolTestsApp.Models.DB;
using SchoolTestsApp.Models.DB.Entities;

namespace SchoolTestsApp.Repository.LoginStudent
{
    public class AuthenticateLogin : ILogin
    {
        private readonly ApplicationContext context;

        public AuthenticateLogin(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task<Student> AuthenticateStudent(string username, string password)
        {
            var succeeded = await context.Students.FirstOrDefaultAsync(u => u.Login == username && u.Password == password);
            return succeeded;
        }
        public async Task<IEnumerable<Student>> getuser()
        {
            return await context.Students.ToListAsync();
        }
    }
}
