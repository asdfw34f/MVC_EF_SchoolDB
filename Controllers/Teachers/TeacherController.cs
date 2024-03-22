
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolTestsApp.Models.DB;
using SchoolTestsApp.Models.DB.Entities;
using SchoolTestsApp.Models.Serialize;
using SchoolTestsApp.Repository.FilesManage;
using SchoolTestsApp.ViewModels;
using QuestionModel = SchoolTestsApp.Models.Serialize.QuestionModel;

namespace SchoolTestsApp.Controllers.Teachers
{
    public class TeacherController : Controller
    {
        private readonly ILogger<TeacherController> _logger;
        Teacher self;
        ApplicationContext context;

        private IFileManger _fileManger;

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

        #region аккаунт
        [Route("/")]
        [Authorize]
        public IActionResult Index()
        {
            return View("Index", self);
        }

        #endregion

        #region добавление тестов

        #endregion

        #region create test
        /*    [Route("/create%%link")]
           [Authorize]
           public IActionResult LinkCreateTest()
           {
               return CreateTest();
           }

           [Route("/create")]
           [Authorize]
           public IActionResult CreateTest()
           {
               return View("CreateTest1", testMain);
           }

           [Route("/create/AddQuetion")]
           [Authorize]
           [HttpPost]
           public IActionResult addAnswer(int questionID)
           {
               return RedirectToAction("CreateTest");
           }

           [Authorize]
           public IActionResult BlankSentence()
           {
               return PartialView("PartialQuestion", new QuestionModel());
           }
        */

        #endregion

        [Route("Teacher/ViewClass/{id}")]
        [Authorize]
        public async Task<IActionResult> ViewClass(int id)
        {
            var classModel = await context.Classes.FindAsync(id);
            classModel.Students = await context.Students.Where(s=>s.ClassId==classModel.id).ToListAsync();

            return View(classModel);
        }

        [Route("Teacher/ViewStudent/{id}")]
        [Authorize]
        public async Task<IActionResult> ViewStudent(int id)
        {
            var studentModel = await context.Students.FindAsync(id);
            studentModel.HistoryTests = await context.History_Tests.Where(h=>h.StudentId == studentModel.id).ToListAsync();
            return View(studentModel);
        }
    }
}