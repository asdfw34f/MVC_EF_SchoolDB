using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolTestsApp.AuthenticationModule;
using SchoolTestsApp.Models.DB;
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
            testsModel = viewModel.ReadFromDBAsync(_context, self.ClassId).Result;
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
            var ht = _context.History_Tests.Where(h => h.StudentId == self.id).ToList();
            List<int> idsHt = new List<int>();
            foreach (var test in ht)
            {
                idsHt.Add(test.TestID);
            }

            for (int i = 0; i < testsModel.Count; i++)
            {
                if (idsHt.Contains(testsModel[i].idTest))
                {
                    testsModel.Remove(testsModel[i]);
                }
            }
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
        public async Task<IActionResult> ViewPostTestAsync(TestViewModelToShow show, int id)
        {
            float countRight = 0;
            var answers = show.answers;

            show.Test = testsModel.Where(t => t.idTest == id).Single().Test;

            for (int i = 0; i< show.Test.Questions.Count; i++)
            {
                if (answers[i] == show.Test.Questions[i].RightAnswer)
                {
                    countRight++;
                }
            }
            float proc = 0;
            if(countRight != 0)
            {
                proc =  countRight / show.Test.Questions.Count;
            }
            
            int mark = 2;
            if (proc < 0.6)
            {
                mark = 2;
            }
            else if(proc < 0.8)
            {
                mark = 3;
            }
            else if (proc > 0.8 && proc < 0.9)
            {
                mark = 4;
            }
            else
            {
                mark = 5;
            }
            
            await _context.History_Tests.AddAsync(new Models.DB.Entities.HistoryTests()
            {
                TestID = id,
                StudentId = (int)Account.GetID(),
                Mark = mark
            });

            await _context.SaveChangesAsync();
            string head = "";
            if (mark == 2)
            {
                head = "Вы не прошли тест";
            }
            else
            {
                head = "Тест пройден";
            }
            TestCompleteViewModel model = new TestCompleteViewModel() { mark = mark, header = head, countRightAnswer = Convert.ToInt32(countRight) };
            return View("CompleteTest", model);
        }

        [Authorize]
        [Route("Students/ViewDoneTests/{id?}")]
        public async Task<IActionResult> ViewDoneTests(int id)
        {
            var history = await _context.History_Tests.Where(h => h.Student.ClassId == id && h.StudentId ==Account.GetID()).ToListAsync();
            for (int i =0; i < history.Count; i++)
            {
                var t = await _context.Tests.FindAsync(history[i].TestID);
                history[i].Test = t;
            }
            return View(history);
        }
    }
}