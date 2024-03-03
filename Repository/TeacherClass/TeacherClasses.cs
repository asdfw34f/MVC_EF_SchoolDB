using AccountLibrary.Serviece;
using SchoolTestsApp.Models.DB;
using SchoolTestsApp.Models.DB.Entities;

namespace SchoolTestsApp.Repository.TeacherClass
{
    public static class TeacherClasses
    {
        public static List<Class>? GetClasses(ApplicationContext context, int id)
        {
            var res =  context.Classes.Where(c => c.TeacherId == Manager.GetId()).ToList();
            var r = (from c 
                )
            return res;
        }
    }
}
