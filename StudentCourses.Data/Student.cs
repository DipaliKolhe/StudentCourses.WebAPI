using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentCourses.Data
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Required]
        [StringLength(256, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(256, MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }

        public string FullName
        {
            get
            {
                return LastName + ", " + FirstName;
            }
        }

        public int Age
        {
            get
            {
                // Save today's date.
                var today = DateTime.Today;

                // Calculate the age.
                var age = today.Year - BirthDate.Year;

                // Go back to the year the person was born in case of a leap year
                if (BirthDate.Date > today.AddYears(-age))
                    age--;
                return age;
            }
        }

    }
}
