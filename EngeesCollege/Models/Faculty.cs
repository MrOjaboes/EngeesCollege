using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EngeesCollege.Models
{
    public class Faculty
    {
        public int FacultyId { get; set; }
        [Required]
        public string Name { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = " Date Added")]
        public DateTime DateAdded { get; set; }
    }
}