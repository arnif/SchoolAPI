using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesAPI.Models
{
    public class CourseInstanceSimpleDTO
    {
        public int ID { get; set; }
        public string CourseID { get; set; }
        public string Semester { get; set; }
    }
}
