using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EngeesCollege.Models
{

    public enum Grade { A1, B2, B3, C4, C5, C6, D7, E8, F9 }

    public class Result
    {
        public int ResultId { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Insertion Date")]
        public DateTime DateInserted { get; set; }
        public Grade? Grade { get; set; }
        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }
    }
}
