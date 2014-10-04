using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesAPI.Models
{
    public class GradesFromProjectsDTO
    {
        public float Average { get; set; }
        public float TotalWeight { get; set; }
        public float AquiredGrade { get; set; }

        public List<GradeDTO> GradeList { get; set; }

    }
}
