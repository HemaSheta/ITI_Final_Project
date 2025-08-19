using Microsoft.EntityFrameworkCore;
using Project_Final_ITI.Models;

namespace Project_Final_ITI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Grade> Grades { get; set; }

        public DbSet<Enrollment> Enrollments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User - Course (Instructor)
            modelBuilder.Entity<Course>()
                .HasOne(c => c.User)
                .WithMany(u => u.Courses)
                .HasForeignKey(c => c.InstructorID)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            // Enrollment (Many-to-Many: Student - Course)
            modelBuilder.Entity<Enrollment>()
                .HasKey(e => new { e.StudentId, e.CourseId });

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.User)
                .WithMany(u => u.Enrollments)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            // Course - Session
            modelBuilder.Entity<Session>()
                .HasOne(s => s.Course)
                .WithMany(c => c.Sessions)
                .HasForeignKey(s => s.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            // Session - Grade
            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Session)
                .WithMany(s => s.Grades)
                .HasForeignKey(g => g.SessionId)
                .OnDelete(DeleteBehavior.Cascade);

            // User (Trainee) - Grade
            modelBuilder.Entity<Grade>()
                .HasOne(g => g.User)
                .WithMany(u => u.Grades)
                .HasForeignKey(g => g.TraineeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Course>()
           .HasIndex(c => c.CourseName)
           .IsUnique();


            modelBuilder.Entity<User>().HasData(
            new User{UserId = 1,UserName = "Dr. Ahmed",Email = "ahmed@iti.com",Role = "Instructor"},
            new User{UserId = 2,UserName = "Sara Ali",Email = "sara@student.com",Role = "Trainee"});

            
            modelBuilder.Entity<Course>().HasData(
            new Course{CourseId = 1,CourseName = "C# Programming",Category = "Programming",InstructorID = 1});

            modelBuilder.Entity<Enrollment>().HasData(
            new Enrollment{StudentId = 2, CourseId = 1  });
        }

    }

}

