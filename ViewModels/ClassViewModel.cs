using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolTestsApp.Models.DB.Entities;

namespace SchoolTestsApp.ViewModels
{
    public class ClassViewModel
    {
        public int classId { get; set; }
        public string classCode { get; set; }

        public List<SelectListItem> classes { get; set; }
    }
}
