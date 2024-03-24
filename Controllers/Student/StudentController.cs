using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Diagnostics.Internal;
using SchoolTestsApp.AuthenticationModule;
using SchoolTestsApp.Models.DB;
using SchoolTestsApp.Models.DB.Entities;
using SchoolTestsApp.Models.Serialize;
using SchoolTestsApp.ViewModels;
using System.Collections.Generic;

namespace SchoolTestsApp.Controllers.Student
{
    public class StudentController : Controller
    {

        private readonly ILogger<StudentController> _logger;
        private ApplicationContext _context;
        private SchoolTestsApp.Models.DB.Entities.Student self = new SchoolTestsApp.Models.DB.Entities.Student();

        List<TestViewModelToShow> testsModel = new List<TestViewModelToShow>(); 
        TestViewModelToShow testViewModelToShow = new TestViewModelToShow();


        public StudentController(ILogger<StudentController> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
            
            if (!(Account.isAuthenticated()) || Account.GetID() == null)
            {
                Redirect("/logout");
            }
            self = _context.Students.Single(s=>s.id==Account.GetID());
            self.Class = _context.Classes.Find(self.ClassId);
            self.HistoryTests = _context.History_Tests.Where(ht=>ht.StudentId==self.id).ToList();
            
            TestViewModel viewModel = new TestViewModel();
            testsModel = viewModel.ReadFromDBAsync(self.ClassId, _context).Result;
        }

        [Route("Student/{id?}")]
        [Authorize]
        public IActionResult Index()
        {
            var id = self.id;

            return View(self);
        }

        [Route("Students/ViewNewTests")]
        [Authorize]
        public IActionResult ViewNewTests()
        {
            return View(testsModel);
        }

        [Route("Students/ViewTest/{id?}")]
        [Authorize]
        public IActionResult ViewTest(int id)
        {
            testViewModelToShow = testsModel.Where(t => t.idTest == id).Single();
            var count = testViewModelToShow.Test.Questions.Count;
            testViewModelToShow.answers = new int[count];

            return View(testViewModelToShow);
        }

        [Authorize]
        [HttpPost]
        [Route("Students/ViewTest/{id?}")]
        public IActionResult ViewPostTest(TestViewModelToShow show)
        {
            int countRight = 0;
            @ViewBag.ResultCheck ="Правильных ответов: " + countRight;
            return View("ViewTest");
        }
    }
}