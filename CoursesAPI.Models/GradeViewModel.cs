using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CoursesAPI.Models
{
    /// <summary>
    /// The class represent the data needed to add a grade
    /// </summary>
    public class GradeViewModel
    {
        /// <summary>
        /// SSN of the student
        /// </summary>
        [Required]
        public string SSN { get; set; }

        /// <summary>
        /// Grade for the student
        /// </summary>
        public float Grade { get; set; }

    }
}
