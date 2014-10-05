using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesAPI.Models
{
    /// <summary>
    /// A DTO class representing Grade entity class with added variables
    /// </summary>
    public class FinalGradeDTO
    {
        /// <summary>
        /// Final grade in the course
        /// </summary>
        public float FinalGrade { get; set; }

        /// <summary>
        /// Where the student is ranked
        /// </summary>
        public int PlaceInStudents { get; set; }

        /// <summary>
        /// How much of the course has been finished 
        /// </summary>
        public float FinalWeight { get; set; }

        /// <summary>
        /// List of all grades from the student in the course
        /// </summary>
        public List<GradesFromProjectsDTO> GradesList { get; set; }

        /// <summary>
        /// PersonsDTO representing the person.
        /// </summary>
        public PersonsDTO Person { get; set; }

        /// <summary>
        /// Variale to tell if the student did pass the course
        /// </summary>
        public bool DidPass { get; set; }

        /// <summary>
        /// Variale to tell if the student did pass the exam
        /// </summary>
        public bool DidPassExam { get; set; }
    }
}
