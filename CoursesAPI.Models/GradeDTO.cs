using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesAPI.Models
{
    public class GradeDTO
    {
        public string SSN { get; set; }
        public float ProjectGrade { get; set; }
        public string ProjectName { get; set; }
        public ProjectDTO Project { get; set; }
    }
}
