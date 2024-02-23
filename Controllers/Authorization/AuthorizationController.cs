using AccountLibrary.Serviece;
using Microsoft.AspNetCore.Mvc;
using SchoolTestsApp.Models.DB;
using SchoolTestsApp.Models.DB.Entities;

namespace SchoolTestsApp.Controllers.Authorization
{
    public class AuthorizationController : Controller
    {
        private ApplicationContext context;

        private readonly Repository.LoginStudent.ILogin _studentLogin;
        private readonly Repository.LoginTeacher.ILogin _teacherLogin;

        public AuthorizationController(ApplicationContext context)
        {
            this.context = context;
            _studentLogin = new Repository.LoginStudent.AuthenticateLogin(context);
            _teacherLogin = new Repository.LoginTeacher.AuthenticateLogin(context);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string username, string password, bool isStudent)
        {
            if (!isStudent)
            {
                var issuccess = _teacherLogin.AuthenticateTeacher(username, password);
                if (issuccess.Result != null)
                {
                    ViewBag.username = string.Format("Successfully logged-in", username);
                    TempData["username"] = "Ahmed";

                    Manager.Init(
                                   issuccess.Result.Name,
                                   issuccess.Result.SecondName,
                                   issuccess.Result.id);

                    return RedirectToRoute("default", new { controller = "Tests", action = "Index" });
                   // return View();

                }
                else
                {
                    ViewBag.username = string.Format("Login Failed ", username);
                    return View();
                }
            }
            else
            {
                var issuccess = _studentLogin.AuthenticateStudent(username, password);
                if (issuccess.Result != null)
                {
                    ViewBag.username = string.Format("Successfully logged-in", username);
                    TempData["username"] = "Ahmed";
                    return RedirectToAction("Index", "Layout");
                }
                else
                {
                    ViewBag.username = string.Format("Login Failed ", username);
                    return View();
                }
            }
        }
    }
}