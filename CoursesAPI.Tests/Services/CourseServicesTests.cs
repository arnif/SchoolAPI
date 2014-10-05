using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoursesAPI.Services.Services;
using CoursesAPI.Tests.MockObjects;
using CoursesAPI.Tests.TestExtensions;
using CoursesAPI.Models;
using CoursesAPI.Services.DataAccess;
using CoursesAPI.Services.Models.Entities;
using CoursesAPI.Services.Extensions;
using System.Diagnostics;
using System.Collections.Generic;


namespace CoursesAPI.Tests.Services
{
    [TestClass]
    public class CourseServicesTests
    {
        private CoursesServiceProvider _service;
        private MockUnitOfWork<MockDataContext> _uow;

        private int courseInstanceID;
        private int projectGroupID;

        [TestInitialize]
        public void Setup()
        {
            // TODO: code which will be executed before each test!
            _uow = new MockUnitOfWork<MockDataContext>();
            _service = new CoursesServiceProvider(_uow);

            this.courseInstanceID = 1;
            this.projectGroupID = 1;
            

            var project = new List<Project>
            {
                new Project
                {
                    ID = 1,
                    Name = "1",
                    ProjectGroupID = 1,
                    CourseInstanceID = 1,
                    OnlyIfHigherThanProjectID = 1,
                    Weight = 1,
                    MinGradeToPassCourse = 1,
                    

                }
            };

            var course = new List<CourseInstance>{
                new CourseInstance{
                    ID = 1,
                    CourseID = "1",
                    SemesterID = " "
                }
            };
            var projectGroup = new List<ProjectGroup> { 
                new ProjectGroup{
                    ID = 1,
                    Name = "1",
                    GradedProjectsCount = 0
                }
            };
            var person = new List<Person> { 
                new Person{
                    ID = 1,
                    SSN = "1",
                    Name = "1",
                    Email = "1"
                }
            };
            var grade = new List<Grade>{
                new Grade{
                    ID = 1,
                    ProjectID = 1,
                    ProjectGrade = 1,
                    PersonSSN = "1",
                }
            };
            var courseStudent = new List<CourseStudent> { 
                new CourseStudent{
                    ID = 1,
                    SSN = "1",
                    CourseInstanceID = 1,
                    Status = 1,
                }
            };
            _uow.SetRepositoryData(courseStudent);
            _uow.SetRepositoryData(grade);
            _uow.SetRepositoryData(person);
            _uow.SetRepositoryData(projectGroup);
            _uow.SetRepositoryData(project);
            _uow.SetRepositoryData(course);

        }
        [TestMethod]
        [ExpectedExceptionWithMessage(typeof(ArgumentException), "Name is not legal (required and max length 64)")]
        public void AddToProjectGroupTestNameEqualsNull()
        {
            // Arrange:
            AddProjectGroupViewModel temp = new AddProjectGroupViewModel();
            temp.Name = null;

            // Act:
            _service.AddToProjectGroup(temp);
        }
        [TestMethod]
        [ExpectedExceptionWithMessage(typeof(ArgumentException), "Name is not legal (required and max length 64)")]
        public void AddToProjectGroupTestNameLength()
        {
            // Arrange:
            AddProjectGroupViewModel temp = new AddProjectGroupViewModel();
            String longString = "";
            for (int i = 0; i < 64; i++)
            {
                longString = longString + "1";
            }

            temp.Name = longString;

            // Act:
            _service.AddToProjectGroup(temp);
        }
        [TestMethod]
        public void AddToProjectGroupTestCorrectOutput()
        {
            // Arrange:
            AddProjectGroupViewModel temp = new AddProjectGroupViewModel();

            temp.Name = "Tomcat";
            temp.GradedProjectsCount = 666;
            // Act:
            var blah = _service.AddToProjectGroup(temp);

            // Assert:
            Assert.AreEqual(temp.Name, blah.Name);
            Assert.AreEqual(temp.GradedProjectsCount, blah.GradedProjectsCount);
        }
        [TestMethod]
        [ExpectedExceptionWithMessage(typeof(ArgumentException), "Project group not found, maybe you have not created it?")]
        public void AddProjectTestProjectGroupIsNull()
        {
            // Arrange:
            this.courseInstanceID = 1;
            this.projectGroupID = -1; // This ID will cause an exception being thrown.
            ProjectViewModel model = new ProjectViewModel();

            // Act:
            _service.AddProject(courseInstanceID, projectGroupID, model);
        }
        [TestMethod]
        public void AddProjectTestCorrectOutput()
        {
            // Arrange:
            ProjectViewModel model = new ProjectViewModel();
            model = new ProjectViewModel();
            model.Name = "TTT";
            model.MinGradeToPassCourse = 1;
            model.Weight = 1;
            model.OnlyHigherThanProjectID = 1;

            // Act:
            var blah = _service.AddProject(courseInstanceID, projectGroupID, model);

            // Assert:
            Assert.AreEqual(courseInstanceID, blah.CourseInstance.ID);
        }
        [TestMethod]
        public void AddGradeToProjectTest()
        {
            // Arrange:
            GradeViewModel model = new GradeViewModel();
            model.Grade = 10;
            model.SSN = "1";

            // Act:
                // Count the number of grades before adding a new grade.
            var firstCount = _service.GetAllGradesFromProjectGroup(1, 1, "1").GradeList.Count;
                // Add the new grade.
            _service.AddGradeToProject(1, 1, model);
                // Count the number of grades again.
            float secondCount = _service.GetAllGradesFromProjectGroup(1, 1, "1").GradeList.Count;

            // Assert:
                // Check if the number of grades has risen.
            Assert.AreEqual(++firstCount, secondCount);

        }
        [TestMethod]
        public void GetGradesTestCorrectOutput()
        {
            // Arrange:
            var ssn = "1";

            // Act:
            GradeDTO bla = _service.GetGrades(courseInstanceID, projectGroupID, ssn);

            // Assert:

            Assert.AreEqual(ssn, bla.SSN);

        }
        [TestMethod]
        public void GetAllGradesFromProjectGroupTestCorrectOutput()
        {
            // Arrange:
            
            // Act:
            var blah = _service.GetAllGradesFromProjectGroup(1, 1, "1").GradeList[0].SSN;

            // Assert:
            Assert.AreEqual("1", blah);
        }
        [TestMethod]
        public void NewGetGradesFromCourseTestCorrectOutput()
        { 
            // Arrange:

            // Act:
                // Retrieve the SSN of the person with the grade.
            var blah = _service.NewGetGradesFromCourse(1, "1").Person.SSN;

            // Assert:
            Assert.AreEqual("1", blah);
        }
        [TestMethod]
        public void GetGradesFromAllStudentsInCourseTestCorrectOutput()
        { 
            // Arrange:

            // Act:
                // Retrieve the SSN of the student with the first grade.
            var blah = _service.GetGradesFromAllStudentsInCourse(1)[0].Person.SSN;
            // Assert:
            Assert.AreEqual("1", blah);
        }
        
    }
}
