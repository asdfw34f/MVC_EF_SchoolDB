using Microsoft.AspNetCore.Mvc;
using SchoolTestsApp.Controllers.Teachers;
using SchoolTestsApp.Models.DB.Entities;
using SchoolTestsApp.Models.DB;
using Microsoft.AspNetCore.Authorization;

namespace SchoolTestsApp.Controllers.Students
{
    public class StudentController : Controller
    {

        private readonly ILogger<StudentController> _logger;
        Student self;
        ApplicationContext context;


        public StudentController(ApplicationContext context, ILogger<StudentController> logger )
        {
            _logger = logger;
            this.context = context;
            if (!(AuthenticationModule.Account.isAuthenticated()) || AuthenticationModule.Account.GetID() == null)
            {
                Redirect("/logout");
            }
            self = context.Students.Single(t => t.id == AuthenticationModule.Account.GetID());

        }


        [Authorize]
        [Route("/account")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
