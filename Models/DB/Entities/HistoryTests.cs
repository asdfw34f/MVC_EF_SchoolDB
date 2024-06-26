﻿using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolTestsApp.Models.DB.Entities
{
    public class HistoryTests
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int id { get; set; }
        public int StudentId { get; set; }
        public int TestID { get; set; }
        public int Mark { get; set; }

        public Student Student { get; set; }
        public Test Test { get; set; }
    }
}
