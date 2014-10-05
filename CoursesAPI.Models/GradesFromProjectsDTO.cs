using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesAPI.Models
{
    /// <summary>
    /// A DTO class representing grades from projects
    /// </summary>
    public class GradesFromProjectsDTO
    {
        /// <summary>
        /// Average grade from the projects
        /// </summary>
        public float Average { get; set; }
        
        /// <summary>
        /// How much the projects weigh towards final grade
        /// </summary>
        public float TotalWeight { get; set; }

        /// <summary>
        /// How much grade has been aquired
        /// </summary>
        public float AquiredGrade { get; set; }

        /// <summary>
        /// List of all grades
        /// </summary>
        public List<GradeDTO> GradeList { get; set; }

    }
}
