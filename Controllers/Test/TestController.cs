using Microsoft.AspNetCore.Mvc;
using SchoolTestsApp.Models.Serialize;
using SchoolTestsApp.Models;
using System.Diagnostics;
using SchoolTestsApp.Models.DB;

namespace SchoolTestsApp.Controllers.Test
{
    public class TestController : Controller
    {
        private readonly ILogger<TestController> _logger;
        private ApplicationContext _context;
        int questionID = 1;

        public TestController(ILogger<TestController> logger, ApplicationContext context)
        {
            _logger = logger;

            _context = context;
        }

        public IActionResult Index()
        {
            var model = new TestModel()
            {
                Questions = new List<QuestionModel>()
                {
                      new QuestionModel()
                      {
                          id = 0,
                          Question = "Question 1",
                      },
                      new QuestionModel()
                      {
                          id=1,
                          Question = "Question 2",
                      }
                }
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> AddSentence(TestModel model, int classID)
        {
            var test = new TestModel() { Questions = new List<QuestionModel>() };
            foreach (var question in model.Questions)
            {
                if (question != null && !string.IsNullOrEmpty(question.Question))
                {
                    test.Questions.Add(question);
                }
            }

            return RedirectToAction("Index");
        }

        public IActionResult BlankSentence()
        {
            questionID++;

            return PartialView("_QuestionEditor", new QuestionModel()
            {
                id = questionID,
                Question = "Question text",
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}