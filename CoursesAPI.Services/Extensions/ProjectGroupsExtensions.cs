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
    public static class ProjectGroupsExtensions
    {
        public static ProjectGroup GetProjectGroupByID(this IRepository<ProjectGroup> repo, int id)
        {
            var projectGroup = repo.All().SingleOrDefault(p => p.ID == id);
            if (projectGroup == null)
            {

                HttpResponseMessage h = new HttpResponseMessage();
                h.ReasonPhrase = "Project group not found";
                h.StatusCode = HttpStatusCode.NotFound;
                throw new HttpResponseException(h);
                
            }
            return projectGroup;
        }
    }
}
