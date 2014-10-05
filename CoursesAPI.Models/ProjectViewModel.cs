using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesAPI.Models
{
    /// <summary>
    /// This class represent the data needed to add a project
    /// </summary>
    public class ProjectViewModel
    {
        /// <summary>
        /// Name of the project
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Reference to the ID in Projects, only used if a project is used to higher grade of another project. This variable then references that project.
        /// </summary>
        public int? OnlyHigherThanProjectID { get; set; }

        /// <summary>
        /// Weight of the project
        /// </summary>
        [Required]
        public float Weight { get; set; }
  
        /// <summary>
        /// Min grade to pass the course
        /// </summary>
        public int? MinGradeToPassCourse { get; set; }
    }
}
