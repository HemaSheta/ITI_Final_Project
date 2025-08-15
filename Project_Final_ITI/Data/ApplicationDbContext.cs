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
        public DbSet<Attends> Attendances { get; set; }
        public DbSet<StdHasGrade> StdHasGrades { get; set; }
        public DbSet<StdEnrollsIn> StdEnrollments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Composite key for Attends
            // Attends
            modelBuilder.Entity<Attends>()
                .HasKey(a => new { a.UserId, a.SessionId });

            modelBuilder.Entity<Attends>()
                .HasOne(a => a.User)
                .WithMany(u => u.Attendances)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict); 
            modelBuilder.Entity<Attends>()
                .HasOne(a => a.Session)
                .WithMany(s => s.Attendances)
                .HasForeignKey(a => a.SessionId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<StdEnrollsIn>()
                .HasKey(se => new { se.UserId, se.CourseId });

            modelBuilder.Entity<StdEnrollsIn>()
                .HasOne(se => se.User)
                .WithMany(u => u.Enrollments)
                .HasForeignKey(se => se.UserId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<StdEnrollsIn>()
                .HasOne(se => se.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(se => se.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
            // Composite key for StdHasGrade
            modelBuilder.Entity<StdHasGrade>()
    .HasKey(sg => new { sg.UserId, sg.SessionId });

            modelBuilder.Entity<StdHasGrade>()
                .HasOne(sg => sg.User)
                .WithMany(u => u.Grades)
                .HasForeignKey(sg => sg.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StdHasGrade>()
                .HasOne(sg => sg.Session)
                .WithMany(s => s.Grades)
                .HasForeignKey(sg => sg.SessionId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Course>()
    .HasOne(c => c.Instructor)
    .WithMany(u => u.Courses)
    .HasForeignKey(c => c.UserId)
    .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Session>()
                .HasOne(s => s.Course)
                .WithMany(c => c.Sessions)
                .HasForeignKey(s => s.CourseId)
                .OnDelete(DeleteBehavior.Restrict); 

        }


    }
}
