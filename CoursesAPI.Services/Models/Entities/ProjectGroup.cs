using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoursesAPI.Services.Models.Entities
{
    [Table("ProjectGroups")]
    public class ProjectGroup
    {
        /// <summary>
        /// A database-generated ID of the Project Grouo.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Name of the Project Group, for example "Netpróf" or "Lokapróf"
        /// </summary>
        public String Name { get; set; }


        /// <summary>
        /// A varible used if the ProjectGroup is for example "Netpróf" then GradeProjectsCount tells us how many projects will count (Best of 12 for example)
        /// </summary>
        public int GradedProjectsCount { get; set; }
    }
}
