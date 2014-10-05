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
    public static class ProjectsExtensions
    {
        public static Project GetProjectByID(this IRepository<Project> repo, int id)
        {
            var project = repo.All().SingleOrDefault(p => p.ID == id);
            if (project == null)
            {
                HttpResponseMessage h = new HttpResponseMessage();
                h.ReasonPhrase = "Project not found";
                h.StatusCode = HttpStatusCode.NotFound;
                throw new HttpResponseException(h);
            }
            return project;
        }

        public static List<Project> GetAllProjectsInCourseByCourseID(this IRepository<Project> repo, int id)
        {
            var projects = (from p in repo.All()
                            where p.CourseInstanceID == id
                            select p).ToList();
            if (projects == null)
            {
                HttpResponseMessage h = new HttpResponseMessage();
                h.ReasonPhrase = "Projects not found";
                h.StatusCode = HttpStatusCode.NotFound;
                throw new HttpResponseException(h);
            }
            return projects;
        }
    }
}
