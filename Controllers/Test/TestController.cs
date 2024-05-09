using Microsoft.AspNetCore.Mvc;
using SchoolTestsApp.Models.Serialize;
using SchoolTestsApp.Models;
using System.Diagnostics;
using SchoolTestsApp.Models.DB;
using SchoolTestsApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using SchoolTestsApp.Helpers;

namespace SchoolTestsApp.Controllers.Test
{
    public class TestController : Controller
    {
        private readonly ILogger<TestController> _logger;
        private ApplicationContext _context;

        public TestController(ILogger<TestController> logger, ApplicationContext context)
        {
            _logger = logger;

            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var model = new TestViewModel()
            {
                ClassList = await _context.Classes.ToListAsync(),
                TestModel = new TestModel()
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
                            id = 1,
                            Question = "Question 2",
                        }
                    }
                }
            };

            return View(model);
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddSentence(TestViewModel model)
        {
            var test = new TestViewModel() { TestModel = new TestModel() {  Questions= new List<QuestionModel>() } };
            foreach (var question in model.TestModel.Questions)
            {
                if (question != null && !string.IsNullOrEmpty(question.Question))
                {
                    test.TestModel.Questions.Add(question);
                }
            }
            
            model.WriteToDBAsync(model.TestModel, model.classID, _context);


            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult BlankSentence()
        {
            ItemIndexCount.Count++;

            return PartialView("_QuestionEditor", new QuestionModel()
            {
                id = ItemIndexCount.Count,
                Question = "Question text",
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Authorize]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}