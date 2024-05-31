using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolTestsApp.AuthenticationModule;
using SchoolTestsApp.Controllers.Chat;
using SchoolTestsApp.Models.DB;
using SchoolTestsApp.Models.DB.Entities;
using SchoolTestsApp.ViewModels;

namespace SchoolTestsApp.Controllers.ChatTeacher
{
    public class ChatTeacherController : Controller
    {

        private readonly ILogger<ChatTeacherController> _logger;
        private ApplicationContext _context;
        private SchoolTestsApp.Models.DB.Entities.Teacher self = new SchoolTestsApp.Models.DB.Entities.Teacher();

        public ChatTeacherController(ILogger<ChatTeacherController> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;

            if (!(Account.isAuthenticated()) || Account.GetID() == null)
            {
                Redirect("/logout");
            }
            self = _context.Teachers.Single(s => s.id == Account.GetID());
        }


        public async Task<IActionResult> IndexAsync()
        {
            var cl = await _context.Classes.Where(t => t.TeacherId == Account.GetID()).Select(s => s.id).ToListAsync();

            var st  = await _context.Students.Where(c=> cl.Contains(c.ClassId)).ToListAsync();

            Models.DB.Entities.Chat chat = new Models.DB.Entities.Chat(); 

            List<ChatTeacherViewModel> model = new List<ChatTeacherViewModel>();
            foreach (var s in st)
            {
                try
                {
                    chat = await _context.Chats.Where(h => h.Student_id == s.id).SingleAsync();
                }
                catch
                {
                    var newChat = new Models.DB.Entities.Chat()
                    {
                        Student_id = s.id,
                        Teacher_id = self.id
                    };
                    await _context.Chats.AddAsync(newChat);
                    await _context.SaveChangesAsync();
                    chat = await _context.Chats.Where(h => h.Student_id == s.id).SingleAsync();
                }

                var hc = await _context.History_Chats.Where(c => c.Chat_id == chat.id).ToListAsync();
                hc.Sort((x, y) => x.atDate.CompareTo(y.atDate));

                var vm = new ChatViewModel() { ChatId = chat.id, Messages=hc, StudentId=s.id, TeacherId=self.id };

                model.Add(
                    new ChatTeacherViewModel()
                    {
                        Chat = vm,
                        Student = s
                    });
            }

            return View("Index", model);
        }
    }
}