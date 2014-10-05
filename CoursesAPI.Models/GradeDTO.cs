using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesAPI.Models
{
    /// <summary>
    /// A DTO class for the grade entity class
    /// </summary>
    public class GradeDTO
    {
        /// <summary>
        /// SSN of the student
        /// </summary>
        public string SSN { get; set; }

        /// <summary>
        /// The grade from the project
        /// </summary>
        public float ProjectGrade { get; set; }

        /// <summary>
        /// Name of the project
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// ProjectDTO of the project
        /// </summary>
        public ProjectDTO Project { get; set; }
    }
}
