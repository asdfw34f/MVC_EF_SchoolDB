using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolTestsApp.AuthenticationModule;
using SchoolTestsApp.Models.DB;
using SchoolTestsApp.Models.DB.Entities;
using SchoolTestsApp.ViewModels;

namespace SchoolTestsApp.Controllers.Chat
{
    public class ChatController : Controller
    {
        private readonly ILogger<ChatController> _logger;
        private ApplicationContext _context;
        private SchoolTestsApp.Models.DB.Entities.Student self = new SchoolTestsApp.Models.DB.Entities.Student();

        public ChatController(ILogger<ChatController> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;

            if (!(Account.isAuthenticated()) || Account.GetID() == null)
            {
                Redirect("/logout");
            }
            self = _context.Students.Single(s => s.id == Account.GetID());
            self.Class = _context.Classes.Find(self.ClassId);
        }

        public async Task<IActionResult> IndexAsync()
        {
            var chat = await _context.Chats.Where(s => s.Student_id == self.id).SingleAsync();
            var messages = await _context.History_Chats.Where(c => c.Chat_id == chat.id).ToListAsync();
            ChatViewModel model = new ChatViewModel();
            model.ChatId = chat.id;
            model.StudentId = self.id;
            model.TeacherId = chat.Teacher_id;
            model.Messages = messages;
            
            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> SendAsync(string messg, string chatID)
        {
            var m = new History_Chat();
            m.atDate = DateTime.Now;
            m.isTeacher = false;
            m.Chat_id = int.Parse(chatID);
            m.Message = messg;

            await _context.History_Chats.AddAsync(m);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

    }
}
