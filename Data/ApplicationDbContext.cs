
using Microsoft.EntityFrameworkCore;
using StudentAccountmvc.Models;

namespace StudentAccountmvc.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Assignment> Assignments { get; set; }

        public DbSet<Result> Results { get; set; }

        public DbSet<Attendance> Attendances { get; set; }

        public DbSet<Fees> Fees { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>()
                .ToTable("Students");

            modelBuilder.Entity<Course>()
                .ToTable("Courses");

            modelBuilder.Entity<Assignment>()
                .ToTable("Assignments");

            modelBuilder.Entity<Result>()
                .ToTable("Results");

            modelBuilder.Entity<Attendance>()
                .ToTable("Attendance");

            modelBuilder.Entity<Fees>()
                .ToTable("Fees");
        }
    }
}

