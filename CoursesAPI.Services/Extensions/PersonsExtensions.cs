using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoursesAPI.Services.DataAccess;
using CoursesAPI.Services.Models.Entities;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using System.Net;

namespace CoursesAPI.Services.Extensions
{
    public static class PersonsExtensions
    {
        public static Person GetPersonBySSN(this IRepository<Person> repo, string ssn)
        {
            var person = repo.All().SingleOrDefault(p => p.SSN == ssn);
            if (person == null)
            {
                HttpResponseMessage h = new HttpResponseMessage();
                h.ReasonPhrase = "Person not found";
                h.StatusCode = HttpStatusCode.NotFound;
                throw new HttpResponseException(h);
            }
            return person;
        }
    }
}
