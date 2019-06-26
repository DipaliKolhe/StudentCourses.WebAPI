using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentCourses.Data
{
    public class SchoolDBContext: DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        public SchoolDBContext(DbContextOptions options) :base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Enrollment>().HasKey(sc => new { sc.StudentId, sc.CourseId });
            modelBuilder.Entity<Enrollment>().HasOne(sc => sc.Student).WithMany(s => s.Enrollments).HasForeignKey(sc => sc.StudentId);
            modelBuilder.Entity<Enrollment>().HasOne(sc => sc.Course).WithMany(c => c.Enrollments).HasForeignKey(sc => sc.CourseId);           
        }
    }
}
