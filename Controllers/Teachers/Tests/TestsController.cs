using AccountLibrary.Serviece;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolTestsApp.Models.DB;
using SchoolTestsApp.Models.DB.Entities;
using SchoolTestsApp.Repository.TeacherClass;

namespace SchoolTestsApp.Controllers.Teachers.Tests
{
    public class TestsController : Controller
    {
        private readonly ILogger<TestsController> _logger;

        ApplicationContext context;

        public TestsController(ApplicationContext context, ILogger<TestsController> logger)
        {

            _logger = logger;
            this.context = context;

        }
        
        public IActionResult Index()
        {
            var classes = new List<SelectListItem>();
            var classesRes = TeacherClasses.GetClasses(
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

            ViewBag.Classes = classes;
            return View();
        }

        [HttpPost]
        public IActionResult Send(byte[] file, string classId, string title) 
        {
            context.Tests.Add(
                new Models.DB.Entities.Test()
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
    }
}
