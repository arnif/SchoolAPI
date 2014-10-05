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
    public static class CourseInstancesExtensions
    {
        public static CourseInstance GetCourseInstanceByID(this IRepository<CourseInstance> repo, int id)
        {
            var courseInstance = repo.All().SingleOrDefault(c => c.ID == id);
            if (courseInstance == null)
            {
                HttpResponseMessage h = new HttpResponseMessage();
                h.ReasonPhrase = "Course not found";
                h.StatusCode = HttpStatusCode.NotFound;
                throw new HttpResponseException(h);
            }
            return courseInstance;
        }
    }
}
