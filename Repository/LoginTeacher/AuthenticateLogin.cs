using Microsoft.EntityFrameworkCore;
using SchoolTestsApp.Models.DB;
using SchoolTestsApp.Models.DB.Entities;

namespace SchoolTestsApp.Repository.LoginTeacher
{
    public class AuthenticateLogin : ILogin
    {
        private readonly ApplicationContext context;

        public AuthenticateLogin(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task<Teacher> AuthenticateTeacher(string username, string password)
        {
            var succeeded = await context.Teachers.FirstOrDefaultAsync(u => u.Login == username && u.Password == password);
            return succeeded;
        }
        public async Task<IEnumerable<Teacher>> getuser()
        {
            return await context.Teachers.ToListAsync();
        }
    }
}
