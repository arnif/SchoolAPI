using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesAPI.Models
{
    public class ProjectViewModel
    {
        [Required]
        public string Name { get; set; }
        public int? OnlyHigherThanProjectID { get; set; }


        [Required]
        public float Weight { get; set; }        
        public int? MinGradeToPassCourse { get; set; }
    }
}
