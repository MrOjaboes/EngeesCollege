using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EngeesCollege.Models
{
    public enum Category
    {
        Science,Art,Commercial
    }
    public class Course
    {
        public int CourseID { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name ="Course Code")]
        public string CourseCode { get; set; }

        public Category? Category { get; set; }

        [Range(0, 6)]
        public int Credits { get; set; }

        public int DepartmentID { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<Instructor> Instructors { get; set; }
        public virtual ICollection<Result> Results { get; set; }
    }
}