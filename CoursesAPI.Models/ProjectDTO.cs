using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesAPI.Models
{
    public class ProjectDTO
    {
        public int ID { get; set; }
        public CourseInstanceSimpleDTO CourseInstance { get; set; }
        public ProjectGroupDTO ProjectGroup { get; set; }
        public float Weight { get; set; }
        public int? MinGradeToPassCourse { get; set; }
        public int? OnlyHigherThanProjectID { get; set; }


    }
}
