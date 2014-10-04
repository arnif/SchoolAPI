using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoursesAPI.Services.DataAccess;
using CoursesAPI.Services.Models.Entities;

namespace CoursesAPI.Services.Extensions
{
    public static class PersonsExtensions
    {
        public static Person GetPersonBySSN(this IRepository<Person> repo, string ssn)
        {
            var person = repo.All().SingleOrDefault(p => p.SSN == ssn);
            if (person == null)
            {
                throw new ArgumentException("Person not found, please try again.");
            }
            return person;
        }
    }
}
