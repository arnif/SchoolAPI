using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesAPI.Services.Models.Entities
{

    [Table("Projects")]
    public class Project
    {
        /// <summary>
        /// A database-generated ID of the Projects.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Name of the project for example "Skilaverkefni 2"
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// A Foreign Key reference to the ID in ProjectGroup table, tells us what kind of project this is.
        /// </summary>
        public int ProjectGroupID { get; set; }

        /// <summary>
        /// A Foreign Key reference to the ID in CourseInstance, tells us what course the project was handed out in
        /// </summary>
        public int CourseInstanceID { get; set; }

        /// <summary>
        /// a Foreign Key reference to the ID in Projects, only used if a project is used to higher grade of another project. This variable then references that project.
        /// </summary>
        public int? OnlyIfHigherThanProjectID { get; set; }

        /// <summary>
        /// How much weight does the project have of the course
        /// </summary>
        public float Weight { get; set; }

        /// <summary>
        /// The min grade that students must get to pass the course, used for example where the project is a final exam. 
        /// </summary>
        public int? MinGradeToPassCourse { get; set; }

    }
}
