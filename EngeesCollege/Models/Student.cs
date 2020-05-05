using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EngeesCollege.Models
{
    public enum Section
    {
        Art,Science,Commercial
    }
    public class Student
    {
        public int ID { get; set; }
        [Required] 
        [StringLength(50)]
        [Display(Name = "Last Name")] 
        public string LastName { get; set; }
        [Required] [StringLength(50)]  
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        
        [Required]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public Gender? Gender { get; set; }
        public Section? Section { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)] 
        [Display(Name = "Enrollment Date")] 
        public DateTime EnrollmentDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Of Birth")]
        public DateTime DoB { get; set; }


        [Display(Name = "Full Name")] 
        public string FullName { get { return FirstName + ", " + LastName; } }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<Result> Results { get; set; }
    }
}