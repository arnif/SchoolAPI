using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesAPI.Models
{
    public class PersonsDTO
    {
        /// <summary>
        /// A database-generated ID of the person.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// The SSN of the person.
        /// </summary>
        public string SSN { get; set; }

        /// <summary>
        /// The full name of the person.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The email of the person.
        /// </summary>
        public string Email { get; set; }
    }
}
