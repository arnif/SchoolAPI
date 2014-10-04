using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesAPI.Models
{
    public class AddProjectGroupViewModel
    {
        [Required]
        public string Name { get; set; }

        public int GradedProjectsCount { get; set; }

    }
}
