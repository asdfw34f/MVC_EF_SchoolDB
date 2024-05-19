using SchoolTestsApp.Models.Serialize;
using System.Xml.Serialization;
using SchoolTestsApp.Models.DB;
using SchoolTestsApp.Models.DB.Entities;
using System.Xml;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using Xceed.Words.NET;

namespace SchoolTestsApp.ViewModels
{
    public class TestViewModel
    {
        public TestModel TestModel { get; set; } = new TestModel();
        public List<Class> ClassList { get; set; } = new List<Class>();
        public int classID { get; set; }

        public TestViewModel()
        {
        }

        /*    protected byte[] Serialize(TestModel test)
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(TestModel));


                using (FileStream fs = new FileStream("temp.xml", FileMode.OpenOrCreate))
                {
                    xmlSerializer.Serialize(fs, test);
                    Console.WriteLine("Object has been serialized");
                    Console.ReadKey();

                }   
            }*/

        protected TestModel? DeserializeXmlFromByteArray(byte[] byteArray)
        {
            XmlDocument xmlDocument = new XmlDocument();

            using (MemoryStream stream = new MemoryStream(byteArray))
            {
                xmlDocument.Load(stream);

                XmlSerializer serializer = new XmlSerializer(typeof(TestModel));
                return serializer.Deserialize(stream) as TestModel;
            }
        }

        public async Task<List<TestViewModelToShow?>> ReadFromDBAsync(ApplicationContext _context, int classId = -1, List<int>? idsHt = null)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(TestModel));
            List<Test> tests;
            
            if (classId != -1)
            {
                 tests = await _context.Tests.Where(t => t.Class == classID).ToListAsync();
      

            }
            else
            {
                 tests = await _context.Tests.ToListAsync();

            }

            List<TestViewModelToShow?> result = new List<TestViewModelToShow?>();
            foreach (var t in tests)
            {
                byte[] bytes = t.TestFile;

                using (FileStream fs = new FileStream("temp.xml", FileMode.OpenOrCreate))
                {
                    await fs.WriteAsync(bytes, 0, bytes.Length);
                }
                try
                {
                    using (var reader = new StreamReader("temp.xml"))
                    {
                        TestModel? item = xmlSerializer.Deserialize(reader) as TestModel;
                        result.Add(new TestViewModelToShow() {Test = item, idTest=t.id});
                    }
                }
                catch (Exception ex)
                {
                    await Console.Out.WriteLineAsync(ex.Message);
                }
                File.Delete("temp.xml");
            }
            return result;

        }
        protected async Task WriteBytes(Stream fs, byte[] dataArray)
        {
            // Write the data to the file, byte by byte.
            for (int i = 0; i < dataArray.Length; i++)
            {
                fs.WriteByte(dataArray[i]);
            }

            // Set the stream position to the beginning of the file.
            fs.Seek(0, SeekOrigin.Begin);

            // Read and verify the data.
            for (int i = 0; i < fs.Length; i++)
            {
                if (dataArray[i] != fs.ReadByte())
                {
                    Console.WriteLine("Error writing data.");
                    return;
                }
            }
            Console.WriteLine("The data was written to {0} " +
                "and verified.");
        }

        public async Task WriteToDBAsync(TestModel test, int classID, ApplicationContext _context)
        {
          

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(TestModel));

            using (FileStream fs = new FileStream("temp.xml", FileMode.Create))
            {
                xmlSerializer.Serialize(fs, test);
                Console.WriteLine("Object has been serialized");
            }
            using (FileStream fs = new FileStream("temp.xml", FileMode.Open))
            {
                var bytes = ReadFully(fs);
                await _context.Tests.AddAsync(
                           new Test()
                           {
                               Title = test.Title,
                               TestFile = bytes,
                               Class = classID,
                           });
                await _context.SaveChangesAsync();
            }
            File.Delete("temp.xml");
        }

        protected byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
   
}