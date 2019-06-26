using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace StudentCourses.Data
{
    public class Course
    {
        public Course()
        {
            Enrollments = new HashSet<Enrollment>();
        }
        [Key]
        public int CourseId { get; set; }

        [StringLength(256, MinimumLength = 1)]
        public string Name { get; set; }

        public int Credits { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
