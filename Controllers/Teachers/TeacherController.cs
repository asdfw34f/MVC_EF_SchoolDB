using AccountLibrary.Serviece;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolTestsApp.Models.DB;
using SchoolTestsApp.Models.DB.Entities;
using SchoolTestsApp.Repository.FilesManage;
using SchoolTestsApp.Repository.TeacherClass;

namespace SchoolTestsApp.Controllers.Teachers
{
    public class TeacherController : Controller
    {
        private readonly ILogger<TeacherController> _logger;
        Teacher self;
        ApplicationContext context;

        private IFileManger _fileManger;
        SelectList classes;
        List<Class>? classesRes;

        public TeacherController(ApplicationContext context, ILogger<TeacherController> logger, IFileManger fileManger)
        {
            this.context = context;

            _logger = logger;

            if (!Manager.isAuthenticated)
            {
                Redirect("/logout");
            }
            else
            {
                self = context.Teachers.Single(t => t.id == Manager.GetId());
            }

            _fileManger = fileManger;

            classesRes = TeacherClasses.GetClasses(
                context, Manager.GetId());
            if (classesRes != null)
            {
                classes = new SelectList(classesRes, "Id", "ClassCode");
            }
            ViewData["Classes"] = classes;
            /*
        if (classesRes.Count() > 0)
        {
            foreach (var c in classesRes)
            {
                classes.Add(new SelectListItem()
                {
                    Text = c.ClassCode,
                    Value = c.id.ToString()
                });
            }
        }*/

        }

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
            ViewBag.ClassId = classes;
            return View("AddTest");
        }

        [Route("/")]
        [HttpPost]
        [Authorize]
        public IActionResult PostTest(IFormFile file, string classId, string title)
        {
            
            try{
                if (file != null && file.Length > 0)
                {

                    if (_fileManger.WriteToDatabase(file, title, classId, context).Result)
                    {
                        ViewBag.FileDone = "Тест успешно отправлен";
                    }
                    else
                    {
                        ViewBag.FileDone = "Тест не отправлен.\n Server error";
                    }
                    return View("AddTest");
                }
                else
                {
                    return BadRequest("Необходимо выбрать файл для загрузки.");
                }
            }
            catch(Exception ex)
            {
                ViewBag.FileDone = $"{ex.Message}";
                return View("AddText");
            }
        }
        #endregion
    }
}