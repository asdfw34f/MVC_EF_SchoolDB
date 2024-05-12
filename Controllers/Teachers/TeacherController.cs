
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Diagnostics.Internal;
using SchoolTestsApp.AuthenticationModule;
using SchoolTestsApp.Models.DB;
using SchoolTestsApp.Models.DB.Entities;
using SchoolTestsApp.Models.Serialize;
using SchoolTestsApp.Repository.FilesManage;
using SchoolTestsApp.ViewModels;
using System.Drawing;
using Xceed.Words.NET;
using QuestionModel = SchoolTestsApp.Models.Serialize.QuestionModel;

namespace SchoolTestsApp.Controllers.Teachers
{
    public class TeacherController : Controller
    {
        private readonly ILogger<TeacherController> _logger;
        Teacher self;
        ApplicationContext context;

        private IFileManger _fileManger;

        TestViewModel viewModel = new TestViewModel();
        List<TestViewModelToShow> testsModel = new List<TestViewModelToShow>();

        public TeacherController(ApplicationContext context, ILogger<TeacherController> logger, IFileManger fileManger)
        {
            this.context = context;
            _logger = logger;
            _fileManger = fileManger;

            if (!(AuthenticationModule.Account.isAuthenticated()) || AuthenticationModule.Account.GetID() == null)
            {
                Redirect("/logout");
            }
            self = context.Teachers.Single(t => t.id == AuthenticationModule.Account.GetID());
            self.Classes = context.Classes.Where(c => c.TeacherId == self.id).ToList();

        }

        [Route("/Teacher/{id?}")]
        [Authorize]
        public IActionResult Index()
        {
            var id = self.id;
            return View("Index", self);
        }

        [Route("Teacher/ViewClass/{id}")]
        [Authorize]
        public async Task<IActionResult> ViewClass(int id)
        {
            var classModel = await context.Classes.FindAsync(id);
            classModel.Students = await context.Students.Where(s => s.ClassId == classModel.id).ToListAsync();

            return View(classModel);
        }

        [Route("Teacher/ViewStudent/{id}")]
        [Authorize]
        public async Task<IActionResult> ViewStudent(int id)
        {
            var studentModel = await context.Students.FindAsync(id);

            studentModel.HistoryTests = await context.History_Tests.Where(h => h.StudentId == id).ToListAsync();
            foreach (var test in studentModel.HistoryTests)
            {
                test.Test = await context.Tests.FindAsync(test.TestID);
            }
            return View(studentModel);
        }



        [Route("Teacher/Tests/{id}")]
        [Authorize]
        public async Task<IActionResult> Tests(int id)
        {
            testsModel = viewModel.ReadFromDBAsync(context, id).Result;
            return View("Tests", testsModel);
        }

        [Route("Teacher/Test/Download")]
        [Authorize]
        public async Task<IActionResult> DownloadTest(int id)
        {
            testsModel = viewModel.ReadFromDBAsync(context).Result;

            var testmodel = testsModel.Where(t=>t.idTest==id).First();
                string text = "";
            int indexQuestion = 0;
            foreach (var question in testmodel.Test.Questions)
            {
                indexQuestion++;
                text += $"{indexQuestion}) {question.Question}\n\t1){question.Answer1};\n\t2){question.Answer2};\n\t3){question.Answer3};\n\t4){question.Answer4}.\n\n";
            }

            MemoryStream stream = new MemoryStream();
            DocX doc = DocX.Create(stream);

            Xceed.Document.NET.Paragraph par = doc.InsertParagraph();
            par.Append(text).Font("Times New Roman").FontSize(18).Color(Color.Black).Bold();

            doc.Save();

            return File(stream.ToArray(), System.Net.Mime.MediaTypeNames.Application.Octet, $"{testmodel.Test.Title}.docx");


        }
    }
}