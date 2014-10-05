using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesAPI.Models
{
    /// <summary>
    /// This class represents the data that is needed to add a Project Group
    /// </summary>
    public class AddProjectGroupViewModel
    {
        /// <summary>
        /// Name of the group (ex. Netprof)
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// How many projects in the group will count towards final grade
        /// </summary>
        public int GradedProjectsCount { get; set; }

    }
}
