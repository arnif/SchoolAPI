using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesAPI.Models
{
    /// <summary>
    /// A DTO class representing the Project entity class
    /// </summary>
    public class ProjectDTO
    {
        /// <summary>
        /// Database generated ID of the project
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// CourseInstanceSimpleDTO
        /// </summary>
        public CourseInstanceSimpleDTO CourseInstance { get; set; }

        /// <summary>
        /// ProjectGroupDTO
        /// </summary>
        public ProjectGroupDTO ProjectGroup { get; set; }

        /// <summary>
        /// Weight of the project
        /// </summary>
        public float Weight { get; set; }

        /// <summary>
        /// Min grade to pass the course (default: null)
        /// </summary>
        public int? MinGradeToPassCourse { get; set; }

        /// <summary>
        /// Reference to the ID in Projects, only used if a project is used to higher grade of another project. This variable then references that project.
        /// </summary>
        public int? OnlyHigherThanProjectID { get; set; }


    }
}
