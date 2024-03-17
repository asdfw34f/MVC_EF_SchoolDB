
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolTestsApp.Models;
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
        }

        #region Get VoewModels
        private ClassViewModel GetClassViewModel()
        {
            ClassViewModel classViewModel = new ClassViewModel();
            classViewModel.classes = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            var classes = context.Classes.Where(c => c.TeacherId == self.id).ToList();

            foreach (var c in classes)
            {
                classViewModel.classes.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
                {
                    Text = c.ClassCode,
                    Value = c.id.ToString()
                });
            }
            return classViewModel;
        }
        #endregion

        #region аккаунт
        [Route("/account")]
        [Authorize]
        public IActionResult Index()
        {
            return View("Index", self);
        }

        #endregion

        #region добавление тестов

        [Route("/")]
        [Authorize]
        public IActionResult AddTest()
        {
            return View("AddTest", GetClassViewModel());
        }

        [Route("/")]
        [HttpPost]
        [Authorize]
        public IActionResult PostTest(IFormFile file, ClassViewModel viewModel, string title)
        {
            try {
                if (file != null && file.Length > 0)
                {

                    if (_fileManger.WriteToDatabase(file, title, viewModel.classId.ToString(), context).Result)
                    {
                        ViewBag.FileDone = "Тест успешно отправлен";
                    }
                    else
                    {
                        ViewBag.FileDone = "Тест не отправлен.\n Server error";
                    }
                    return View("AddTest", GetClassViewModel());
                }
                else
                {
                    return BadRequest("Необходимо выбрать файл для загрузки.");
                }
            }
            catch (Exception ex)
            {
                ViewBag.FileDone = $"{ex.Message}";
                return View("AddText", GetClassViewModel());
            }
        }
        #endregion

        #region create test
        [Route("/create%%link")]
        [Authorize]
        public IActionResult LinkCreateTest()
        {
            return Redirect("/create");
        }

        [Route("/create")]
        [Authorize]
        public IActionResult CreateTest()
        {
            TestModel model = new TestModel();
            
            return View("CreateTest", model);
        }

        [Route("/create")]
        [Authorize]
        [HttpPost]
        public IActionResult PostCreateTest(QuestionModel test)
        {
            return View();
        }
        #endregion
    }
}