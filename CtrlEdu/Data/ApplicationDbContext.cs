using CtrlEdu.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CtrlEdu.Data
{
    public class ApplicationDbContext:DbContext
    {
       
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EnrollmentModel>()
                .HasKey(em => new { em.UserID, em.CourseID });
            modelBuilder.Entity<EnrollmentModel>()
                .HasOne(c => c.Course)
                .WithMany(u => u.Users)
                .HasForeignKey(u => u.CourseID);
            modelBuilder.Entity<EnrollmentModel>()
                .HasOne(u => u.User)
                .WithMany(c => c.Courses)
                .HasForeignKey(c => c.UserID);
        }
        public DbSet<CourseModel> Courses { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<AssignmentModel> Assignments { get; set; }

        public DbSet<EnrollmentModel> Enrollments { get; set; }
        public DbSet<StudentCourseAssignmentModel> StudentCourseAssignments { get; set; }
        public DbSet<LearningResourceModel> LearningResources { get; set; }
    }
}
