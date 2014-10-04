using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesAPI.Models
{
    public class FinalGradeDTO
    {
        public float FinalGrade { get; set; }
        public int PlaceInStudents { get; set; }
        public float FinalWeight { get; set; }
        public List<GradesFromProjectsDTO> GradesList { get; set; }
        public PersonsDTO Person { get; set; }
        public bool DidPass { get; set; }
        public bool DidPassExam { get; set; }
    }
}
