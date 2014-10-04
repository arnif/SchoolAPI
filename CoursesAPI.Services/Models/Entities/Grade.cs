using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesAPI.Services.Models.Entities
{
    [Table("Grades")]
    public class Grade
    {
        /// <summary>
        /// A database-generated ID of the Grade.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// A Foreign Key that references the ID in the Projects table
        /// </summary>
        public int ProjectID { get; set; }

        /// <summary>
        /// A variable to store the grade the student got for the project
        /// </summary>
        public float ProjectGrade { get; set; }

        /// <summary>
        /// A Foreign Key that references the SSN in the Person table
        /// </summary>
        public string PersonSSN { get; set; }
    }
}
