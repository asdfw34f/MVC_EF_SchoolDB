using AccountLibrary.Serviece;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolTestsApp.Models.DB;
using SchoolTestsApp.Models.DB.Entities;
using SchoolTestsApp.Repository.TeacherClass;

namespace SchoolTestsApp.Controllers.Teachers
{
    public class TeacherController : Controller
    {
        private readonly ILogger<TeacherController> _logger;
        Teacher self;
        ApplicationContext context;

        List<SelectListItem> classes;
        List<Class?> classesRes;

        public TeacherController(ApplicationContext context, ILogger<TeacherController> logger)
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



            classes = new List<SelectListItem>();
            classesRes = TeacherClasses.GetClasses(
                context, Manager.GetId());

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
            }
        }

        #region аккаунт
        [Route("/account")]
        [Authorize]
        public IActionResult Index()
        {
            ViewBag.Classes = classes;

            return View("Index", self);
        }

        #endregion

        #region добавление тестов

        [Route("/")]
        [Authorize]
        public IActionResult AddTest()
        {
            ViewBag.Classes = classes;
            return View("AddTest");
        }

        [HttpPost]
        [Route("/")]
        [Authorize]
        public IActionResult AddTest(byte[] file, string classId, string title)
        {
            context.Tests.Add(
                new Test()
                {
                    Title = title,
                    Class = Convert.ToInt32(classId),
                    TestFile = file
                }
                );
            context.SaveChanges();
            ViewBag.FileDone = "Тест успешно отправлен";
            return View();
        }
        #endregion
    }
}