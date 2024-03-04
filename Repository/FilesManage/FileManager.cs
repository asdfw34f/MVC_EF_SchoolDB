using SchoolTestsApp.Models.DB;
using SchoolTestsApp.Models.DB.Entities;

namespace SchoolTestsApp.Repository.FilesManage
{
    public class FileManager : IFileManger
    {
        public byte[] ReadFromDatabase(string id, ApplicationContext context)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> WriteToDatabase(IFormFile file, string title, string classId, ApplicationContext context)
        {
            try
            {
                using (var ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms);
                    byte[] content = ms.ToArray();

                    context.Tests.Add(
                        new Test()
                        {
                            Title = title,
                            Class = Convert.ToInt32(classId),
                            TestFile = content
                        });
                    await context.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}