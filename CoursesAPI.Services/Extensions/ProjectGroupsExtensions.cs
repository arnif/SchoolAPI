using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoursesAPI.Services.DataAccess;
using CoursesAPI.Services.Models.Entities;

namespace CoursesAPI.Services.Extensions
{
    public static class ProjectGroupsExtensions
    {
        public static ProjectGroup GetProjectGroupByID(this IRepository<ProjectGroup> repo, int id)
        {
            var projectGroup = repo.All().SingleOrDefault(p => p.ID == id);
            if (projectGroup == null)
            {
                throw new ArgumentException("Project group not found, please try again.");
            }
            return projectGroup;
        }
    }
}
