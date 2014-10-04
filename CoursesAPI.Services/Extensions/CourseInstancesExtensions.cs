using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoursesAPI.Services.DataAccess;
using CoursesAPI.Services.Models.Entities;

namespace CoursesAPI.Services.Extensions
{
    public static class CourseInstancesExtensions
    {
        public static CourseInstance GetCourseInstanceByID(this IRepository<CourseInstance> repo, int id)
        {
            var courseInstance = repo.All().SingleOrDefault(c => c.ID == id);
            if (courseInstance == null)
            {
                throw new ArgumentException("Course instance not found, please try again.");
            }
            return courseInstance;
        }
    }
}
