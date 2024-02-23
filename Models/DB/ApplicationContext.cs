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
                         var classes = new List<Class>
{
                new Class(){ClassCode="9А", TeacherId=context.Teachers.First().id},
                new Class(){ClassCode="9Б", TeacherId=context.Teachers.First().id},
                new Class(){ClassCode="9В", TeacherId=context.Teachers.First().id},
                new Class(){ClassCode="8А", TeacherId=context.Teachers.First().id},
            };

            foreach (var c in classes)
            {
                context.Classes.Add(c);
            }

            context.SaveChanges();
             */
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