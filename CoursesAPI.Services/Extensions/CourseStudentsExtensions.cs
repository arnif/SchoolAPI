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
    public static class CourseStudentsExtensions
    {
        public static CourseStudent GetCourseStudent(this IRepository<CourseStudent> repo, int id, string ssn)
        {
            var student = (from s in repo.All()
                           where s.CourseInstanceID == id &&
                           ssn == s.SSN
                           select s).SingleOrDefault();

            if (student == null)
            {
                HttpResponseMessage h = new HttpResponseMessage();
                h.ReasonPhrase = "Student not found";
                h.StatusCode = HttpStatusCode.NotFound;
                throw new HttpResponseException(h);
            }

            return student;
        }
    }
}
