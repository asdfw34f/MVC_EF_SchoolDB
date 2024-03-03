using Microsoft.EntityFrameworkCore;
using SchoolTestsApp.Models.DB.Entities;

namespace SchoolTestsApp.Models.DB
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<HistoryTests> History_Tests { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
         //   Database.EnsureDeleted();
            Database.EnsureCreated();
            /*
            Students.AddRange(new List<Student>
                {
                new Student {
                    Birthday=DateOnly.Parse("12.12.2000"),
                    Name = "Ivan",
                    SecondName = "Stepanov",
                    ThridName = "Maximovich",
                    ClassId = 1,
                    Login = "s1",
                    Password="ps1"
                    
                },
                new Student {
                    Birthday=DateOnly.Parse("02.01.2001"),
                    Name = "Petr",
                    SecondName = "Ivanov",
                    ThridName = "Nikitovich",
                    ClassId = 2,
                    Login = "s2",
                    Password="ps2"
                },
                new Student {
                    Birthday=DateOnly.Parse("10.11.2001"),
                    Name = "Anastasia",
                    SecondName = "Minornaya",
                    ThridName = "Nikitovna",
                    ClassId = 1,
                    Login = "s3",
                    Password="ps3"
                }
            });
            SaveChanges();*/

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Test>(e =>
            {
                e.HasKey(k => k.id);
            });

            modelBuilder.Entity<Teacher>(e =>
            {
                e.HasKey(k => k.id);
            });

            modelBuilder.Entity<Class>(e =>
            {
                e.HasKey(k => k.id);
                e.HasOne(c => c.Teacher)
                .WithMany(t => t.Classes)
                .HasForeignKey(c => c.TeacherId)
                .HasPrincipalKey(t => t.id);
            });

            modelBuilder.Entity<Student>(e =>
            {
                e.HasKey(k => k.id);
                e.HasOne(s => s.Class)
                .WithMany(c => c.Students)
                .HasForeignKey(s => s.ClassId)
                .HasPrincipalKey(c => c.id);
            });

            modelBuilder.Entity<HistoryTests>(e =>
            {
                e.HasKey(k => k.id);
                e.HasOne(h => h.Student)
                .WithMany(s => s.HistoryTests)
                .HasForeignKey(h => h.StudentId)
                .HasPrincipalKey(s => s.id);
                e.HasOne(h => h.Test)
                .WithMany(t => t.HistoryTests)
                .HasForeignKey(h => h.TestID)
                .HasPrincipalKey(t => t.id);
            });
        }
    }
}