using Microsoft.AspNetCore.Mvc;
using SchoolTestsApp.AuthenticationModule;
using SchoolTestsApp.Models.DB;

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
        public async Task<IActionResult> Index(string username, string password)
        {

            var issuccess = await _Login.Authectication(username, password, HttpContext);
            if (issuccess)
            {
                ViewBag.username = string.Format("Successfully logged-in", username);
                TempData["username"] = "Ahmed";
                if (!AuthenticationModule.Account.isStudent())
                {
                    return RedirectToRoute( new {controller="Teacher", action="Index", id = Account.GetID() });
                }
                else
                {
                    return RedirectToRoute( new {controller="Student", action="Index", id = Account.GetID() });
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