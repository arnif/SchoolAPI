using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesAPI.Models
{
    /// <summary>
    /// Simpler DTO for Course instances entity class
    /// </summary>
    public class CourseInstanceSimpleDTO
    {
        /// <summary>
        /// Database generated ID of the course instance
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// String represenatation of the course (ex. T-123-TEST)
        /// </summary>
        public string CourseID { get; set; }

        /// <summary>
        /// What semester the course is tought.
        /// </summary>
        public string Semester { get; set; }
    }
}
