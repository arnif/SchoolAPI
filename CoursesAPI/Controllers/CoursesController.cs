using System.Collections.Generic;
using System.Web.Http;
using System.Net.Http;
using System.Web;
using System.Net;
using CoursesAPI.Models;
using CoursesAPI.Services.DataAccess;
using CoursesAPI.Services.Models.Entities;
using CoursesAPI.Services.Services;
using System.Security.Claims;
using System;
using System.Linq;

namespace CoursesAPI.Controllers
{
	[RoutePrefix("api/courses")]
	public class CoursesController : ApiController
	{
		private readonly CoursesServiceProvider _service;

		public CoursesController()
		{
			_service = new CoursesServiceProvider(new UnitOfWork<AppDataContext>());
		}

        /// <summary>
        /// Get the teachers in a course
        /// </summary>
        /// <param name="courseInstanceID">ID of the course instance</param>
        /// <returns>List of teachers</returns>
		[Route("{courseInstanceID}/teachers")]
		public List<Person> GetCourseTeachers(int courseInstanceID)
		{
			return _service.GetCourseTeachers(courseInstanceID);
		}
		
        /// <summary>
        /// Get the courses in a semester
        /// </summary>
        /// <param name="semester">The semester that you are looking up (ex. 20143)</param>
        /// <returns>List of Courses</returns>
		[Route("semester/{semester}")]
		public List<CourseInstanceDTO> GetCoursesOnSemester(string semester)
		{
			return _service.GetSemesterCourses(semester);
		}

        /// <summary>
        /// Add project group
        /// </summary>
        /// <param name="model">AddProjectGroupViewModel</param>
        /// <returns>Created and ProjectGroupDTO</returns>
        [HttpPost]
        [Authorize(Roles = "teacher")]
        [Route("projectgroup")]
        public HttpResponseMessage AddProjectGroup(AddProjectGroupViewModel model)
        {
            return Request.CreateResponse(HttpStatusCode.Created, _service.AddToProjectGroup(model)); 
        }

        /// <summary>
        /// Add project to course
        /// </summary>
        /// <param name="courseInstanceID">Course instance ID</param>
        /// <param name="projectGroupID">Project Group ID</param>
        /// <param name="model">ProjectViewModel</param>
        /// <returns>Created and ProjectDTO</returns>
        [HttpPost]
        [Authorize(Roles = "teacher")]
        [Route("{courseInstanceID}/project/{projectGroupID}")]
        public HttpResponseMessage AddProject(int courseInstanceID, int projectGroupID, ProjectViewModel model)
        {
            return Request.CreateResponse(HttpStatusCode.Created, _service.AddProject(courseInstanceID, projectGroupID, model));
        }

        /// <summary>
        /// Add grade to a project
        /// </summary>
        /// <param name="courseInstanceID">Course instance ID</param>
        /// <param name="projectID">Project ID</param>
        /// <param name="model">GradeViewModel</param>
        /// <returns>Created</returns>
        [HttpPost]
        [Authorize(Roles = "teacher")]
        [Route("{courseInstanceID}/grade/{projectID}")]
        public HttpResponseMessage AddGradeToProject(int courseInstanceID, int projectID, GradeViewModel model)
        {
            _service.AddGradeToProject(courseInstanceID, projectID, model);
            return Request.CreateResponse(HttpStatusCode.Created);
        }
        
        /// <summary>
        /// Get student grade from a project
        /// </summary>
        /// <param name="courseInstanceID">Course instance ID</param>
        /// <param name="projectID">Project ID</param>
        /// <param name="ssn">SSN of the student</param>
        /// <returns>OK, GradeDTO</returns>
        [HttpGet]
        [Authorize(Roles = "student,teacher")]
        [Route("{courseInstanceID}/grade/{projectID}/{ssn}")]
        public HttpResponseMessage GetGrades(int courseInstanceID, int projectID, string ssn)
        {
            System.Security.Claims.ClaimsPrincipal userClame = new System.Security.Claims.ClaimsPrincipal(User.Identity);

            var userName = GetUserName(userClame);

            if (userClame.IsInRole("teacher"))
            {
                return Request.CreateResponse(HttpStatusCode.OK, _service.GetGrades(courseInstanceID, projectID, ssn));
            }

            if (userClame.IsInRole("student"))
            {
                if (_service.CheckIfCorrectStudent(ssn, userName))
                {
                    return Request.CreateResponse(HttpStatusCode.OK, _service.GetGrades(courseInstanceID, projectID, ssn));
                }
            }
            return Request.CreateResponse(HttpStatusCode.Unauthorized);           
        }

        /// <summary>
        /// Get student grades from project group (ex. Netprof)
        /// </summary>
        /// <param name="courseInstanceID">Course instance ID</param>
        /// <param name="projectGroupID">Project group ID</param>
        /// <param name="ssn">SSN of the student</param>
        /// <returns>OK, GradesFromProjectsDTO</returns>
        [HttpGet]
        [Authorize(Roles = "student,teacher")]
        [Route("{courseInstanceID}/grades/{projectGroupID}/{ssn}")]
        public HttpResponseMessage GetGradesFromProjectGroup(int courseInstanceID, int projectGroupID, string ssn)
        {
            System.Security.Claims.ClaimsPrincipal userClame = new System.Security.Claims.ClaimsPrincipal(User.Identity);

            var userName = GetUserName(userClame);

            if (userClame.IsInRole("teacher"))
            {
                return Request.CreateResponse(HttpStatusCode.OK, _service.GetAllGradesFromProjectGroup(courseInstanceID, projectGroupID, ssn));
            }

            if (userClame.IsInRole("student"))
            {
                if (_service.CheckIfCorrectStudent(ssn, userName))
                {
                    return Request.CreateResponse(HttpStatusCode.OK, _service.GetAllGradesFromProjectGroup(courseInstanceID, projectGroupID, ssn));
                }
            }

            return Request.CreateResponse(HttpStatusCode.Unauthorized);
            
        }

        /// <summary>
        /// Get student grades for a course
        /// </summary>
        /// <param name="courseInstanceID">Course instance ID</param>
        /// <param name="ssn">SSN of the student</param>
        /// <returns>OK, FinalGradeDTO</returns>
        [HttpGet]
        [Authorize(Roles = "student,teacher")]
        [Route("{courseInstanceID}/grades/{ssn}")]
        public HttpResponseMessage GetGradesFromCourse(int courseInstanceID, string ssn)
        {   
            System.Security.Claims.ClaimsPrincipal userClame = new System.Security.Claims.ClaimsPrincipal(User.Identity);

            var userName = GetUserName(userClame);

            if (userClame.IsInRole("teacher"))
            {
                return Request.CreateResponse(HttpStatusCode.OK, _service.NewGetGradesFromCourse(courseInstanceID, ssn));
            }

            if (userClame.IsInRole("student"))
            {
                if (_service.CheckIfCorrectStudent(ssn, userName))
                {
                    return Request.CreateResponse(HttpStatusCode.OK, _service.NewGetGradesFromCourse(courseInstanceID, ssn));
                }
                
            }

            return Request.CreateResponse(HttpStatusCode.Unauthorized);     
        }        

        /// <summary>
        /// Get all grades from all students in a course
        /// </summary>
        /// <param name="courseInstanceID">Course instance ID</param>
        /// <returns>OK, List of FinalGradeDTO</returns>
        [HttpGet]
        [Authorize(Roles="teacher")]
        [Route("{courseInstanceID}/grades")]
        public HttpResponseMessage GetAllGradesFromCourse(int courseInstanceID)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _service.GetGradesFromAllStudentsInCourse(courseInstanceID));
        }

        /// <summary>
        /// Helper function to get the username from the userClame
        /// </summary>
        /// <param name="userClame">Object with all neccessary data</param>
        /// <returns>User name</returns>
        private String GetUserName(ClaimsPrincipal userClame)
        {
            var principle = User as ClaimsPrincipal;
            var student = (from st in principle.Identities.First().Claims.Where(s => s.Type == "name") select st.Value).SingleOrDefault();
            return student;
        }
	}
}
