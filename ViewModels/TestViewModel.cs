using SchoolTestsApp.Models.Serialize;
using System.Xml.Serialization;
using SchoolTestsApp.Models.DB;

namespace SchoolTestsApp.ViewModels
{
    public class TestViewModel
    {
        ApplicationContext _context;
        public TestViewModel(ApplicationContext context)
        {
            _context = context;
        }

        protected byte[] Serialize(TestModel test)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(TestModel));

            using (MemoryStream stream = new MemoryStream())
            {
                xmlSerializer.Serialize(stream, test);

                using (MemoryStream ms = new MemoryStream())
                {
                    byte[] buffer = new byte[16 * 1024];
                    int read;
                    while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, read);
                    }
                    return ms.ToArray();
                }
            }
        }

        protected TestModel? DeserializeXmlFromByteArray(byte[] byteArray)
        {
            using (MemoryStream stream = new MemoryStream(byteArray))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(TestModel));
                return serializer.Deserialize(stream) as TestModel;
            }
        }

        public TestModel? ReadFromDB(int id)
        {
            var test = _context.Tests.Where(t => t.id == id).Single().TestFile;
            return DeserializeXmlFromByteArray(test);
        }

        public void WriteToDB(TestModel test, int classID)
        {
            var bytes = Serialize(test);

            _context.Tests.Add(
                new Models.DB.Entities.Test()
                {
                    Title = test.Title,
                    TestFile = bytes,
                    Class = classID
                });
            _context.SaveChanges();
        }
    }
}