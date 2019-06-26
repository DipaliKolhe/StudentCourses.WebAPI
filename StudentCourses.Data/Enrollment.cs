using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StudentCourses.Data
{
    public class Enrollment
    {
        [Key]
        public int EnrollmentId { get; set; }

        [Key]
        [ForeignKey("Course")]
        public int CourseId { get; set; }

        [Key]
        [ForeignKey("Student")]
        public int StudentId { get; set; }

        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }

    }
}
