using AccountLibrary.Serviece;
using Microsoft.AspNetCore.Mvc;
using SchoolTestsApp.Models.DB;
using SchoolTestsApp.Models.DB.Entities;

namespace SchoolTestsApp.Controllers.Authorization
{
    public class AuthorizationController : Controller
    {
        private ApplicationContext context;

        private readonly Repository.Authentication.ILogin _Login;

        public AuthorizationController(ApplicationContext context)
        {
            this.context = context;
            _Login = new Repository.Authentication.Login(context);
        }


        [Route("/login")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("/login")]
        public IActionResult Index(string username, string password)
        {

            var issuccess = _Login.Authectication(username, password, HttpContext);
            if (issuccess.Result)
            {
                ViewBag.username = string.Format("Successfully logged-in", username);
                TempData["username"] = "Ahmed";
                if (!Manager.GetType())
                {
                    return Redirect("/");

                }
                else
                {
                    // return student view

                }
            }
            else
            {
                    ViewBag.username = string.Format("Login Failed ", username);
                    return View();
            }
            ViewBag.username = string.Format("Login Failed ", username);
            return View();
        }
    }
}