using System.Collections.Generic;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using CoursesAPI.Models;
using CoursesAPI.Services.DataAccess;
using CoursesAPI.Services.Models.Entities;
using CoursesAPI.Services.Services;

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

		[Route("{courseInstanceID}/teachers")]
		public List<Person> GetCourseTeachers(int courseInstanceID)
		{
			return _service.GetCourseTeachers(courseInstanceID);
		}
		
		[Route("semester/{semester}")]
		public List<CourseInstanceDTO> GetCoursesOnSemester(string semester)
		{
			return _service.GetSemesterCourses(semester);
		}

        [HttpPost]
        [Route("projectgroup")]
        public HttpResponseMessage AddProjectGroup(AddProjectGroupViewModel model)
        {
            return Request.CreateResponse(HttpStatusCode.Created, _service.AddToProjectGroup(model)); 
        }

        [HttpPost]
        [Route("{courseInstanceID}/project/{projectGroupID}")]
        public HttpResponseMessage AddProject(int courseInstanceID, int projectGroupID, ProjectViewModel model)
        {
            return Request.CreateResponse(HttpStatusCode.Created, _service.AddProject(courseInstanceID, projectGroupID, model));
        }

        [HttpPost]
        [Route("{courseInstanceID}/grade/{projectID}")]
        public HttpResponseMessage AddGradeToProject(int courseInstanceID, int projectID, GradeViewModel model)
        {
            _service.AddGradeToProject(courseInstanceID, projectID, model);
            return Request.CreateResponse(HttpStatusCode.Created);
        }
        
        [HttpGet]
        [Route("{courseInstanceID}/grade/{projectID}/{ssn}")]
        public HttpResponseMessage GetGrades(int courseInstanceID, int projectID, string ssn)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _service.GetGrades(courseInstanceID, projectID, ssn));
        }

        [HttpGet]
        [Route("{courseInstanceID}/grades/{projectGroupID}/{ssn}")]
        public HttpResponseMessage GetGradesFromProjectGroup(int courseInstanceID, int projectGroupID, string ssn)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _service.GetAllGradesFromProjectGroup(courseInstanceID, projectGroupID, ssn));
        }

        [HttpGet]
        [Route("{courseInstanceID}/grades/{ssn}")]
        public HttpResponseMessage GetGradesFromCourse(int courseInstanceID, string ssn)
        {            
            return Request.CreateResponse(HttpStatusCode.OK, _service.NewGetGradesFromCourse(courseInstanceID, ssn));
        }

        [HttpGet]
        [Route("{courseInstanceID}/grades")]
        public HttpResponseMessage GetAllGradesFromCourse(int courseInstanceID)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _service.GetGradesFromAllStudentsInCourse(courseInstanceID));
        }
	}
}
