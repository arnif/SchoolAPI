using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesAPI.Models
{
    /// <summary>
    /// A DTO class representing the project group entity class
    /// </summary>
    public class ProjectGroupDTO
    {
        /// <summary>
        /// Database generated ID of the project group
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Name of the project group
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// How many projects in the group will count towards final grade
        /// </summary>
        public int GradedProjectsCount { get; set; }
    }
}
