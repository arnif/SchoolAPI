using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoursesAPI.Services.DataAccess;
using CoursesAPI.Services.Models.Entities;

namespace CoursesAPI.Services.Extensions
{
    public static class ProjectsExtensions
    {
        public static Project GetProjectByID(this IRepository<Project> repo, int id)
        {
            var project = repo.All().SingleOrDefault(p => p.ID == id);
            if (project == null)
            {
                throw new ArgumentException("Project not found, please try again.");
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
                throw new ArgumentException("Projects not found, please try again.");
            }
            return projects;
        }
    }
}
