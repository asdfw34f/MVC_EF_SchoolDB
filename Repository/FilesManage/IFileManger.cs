using SchoolTestsApp.Models.DB;

namespace SchoolTestsApp.Repository.FilesManage
{
    public interface IFileManger
    {
        public Task<bool> WriteToDatabase(IFormFile file, string title, string classId, ApplicationContext context);
        public byte[] ReadFromDatabase(string id, ApplicationContext context);
    }
}
