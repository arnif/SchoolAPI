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
        private ProjectViewModel model;

        [TestInitialize]
        public void Setup()
        {
            // TODO: code which will be executed before each test!
            _uow = new MockUnitOfWork<MockDataContext>();
            _service = new CoursesServiceProvider(_uow);

            this.courseInstanceID = 1;
            this.projectGroupID = 1;
            this.model = new ProjectViewModel();
            this.model.Name = "TTT";
            this.model.MinGradeToPassCourse = 1;
            this.model.Weight = 1;
            this.model.OnlyHigherThanProjectID = 1;

            var project = new List<Project>
            {
                new Project
                {
                    ID = this.courseInstanceID,
                    Name = this.model.Name,
                    ProjectGroupID = this.projectGroupID,
                    CourseInstanceID = this.courseInstanceID,
                    OnlyIfHigherThanProjectID = 1,
                    Weight = 1,
                    MinGradeToPassCourse=4,
                    

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
            int courseInstanceID = 1;
            int projectGroupID = -1;
            ProjectViewModel model = new ProjectViewModel();


            // Act:
            _service.AddProject(courseInstanceID, projectGroupID, model);
        }
        [TestMethod]
        public void AddProjectTestCorrectOutput()
        {
            // Arrange:

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
            var firstCount = _service.GetAllGradesFromProjectGroup(1, 1, "1").GradeList.Count;
            _service.AddGradeToProject(1, 1, model);
            float secondCount = _service.GetAllGradesFromProjectGroup(1, 1, "1").GradeList.Count;

            // Assert:

            Assert.AreEqual(++firstCount, secondCount);

        }
        [TestMethod]
        public void GetGradesTestCorrectOutput()
        {
            // Arrange:


            // Act:
            GradeDTO bla = _service.GetGrades(1, 1, "1");

            // Assert:

            Assert.AreEqual("1", bla.SSN);

        }
        [TestMethod]
        public void GetAllGradesFromProjectGroupTestCorrectOutput()
        {
            // Arrange:
            ProjectViewModel temp = new ProjectViewModel();
            temp.Name = "1";
            temp.MinGradeToPassCourse = 1;
            temp.OnlyHigherThanProjectID = 1;
            temp.Weight = 1;
           

            AddProjectGroupViewModel temp2 = new AddProjectGroupViewModel();
            temp2.Name = "1";
            temp2.GradedProjectsCount = 1;
            _service.AddToProjectGroup(temp2);
            _service.AddProject(1, 1, temp);
            // Act:
            var blah = _service.GetAllGradesFromProjectGroup(1, 1, "1").GradeList.GetEnumerator().Current.SSN;

            Debug.WriteLine(blah.ToString());
            // Assert:
            Assert.AreEqual("1", blah);


        }
    }
}
