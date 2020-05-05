using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EngeesCollege.Models
{
    public enum Qualification
    {
        SSCE,OND,NCE,HND,BSc,MSc,PhD,BBA,MBA,MBBS,RN
    }
    public enum MaritalStatus
    {
        Single,Married,Engaged,Divorced
    }
    public enum Gender
    {
        Male,Female
    }
    public class Instructor
    {
        public int ID { get; set; }

        [Required] [Display(Name = "Last Name")] 
        [StringLength(50)] 
        public string LastName { get; set; }

        [Required]  
        [Display(Name = "First Name")] 
        [StringLength(50)] 
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Email")]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

       
        [Display(Name = "Salary")]
        [Column(TypeName ="money")]
       // [StringLength(50)]
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }


        public Qualification? Qualification { get; set; }

        public MaritalStatus? MaritalStatus { get; set; }

        public Gender? Gender { get; set; }


        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = " Date Of Birth")]
        public DateTime DoB { get; set; }

        public int Age { get; set; }

        [Display(Name = "Full Name")] 
        public string FullName { get { return FirstName + ", " + LastName; } }

        public virtual ICollection<Course> Courses { get; set; }
        public virtual OfficeAssignment OfficeAssignment { get; set; }
    }
}