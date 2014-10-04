using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using CoursesAPI.Models;
using CoursesAPI.Services.DataAccess;
using CoursesAPI.Services.Models.Entities;
using CoursesAPI.Services.Extensions;

namespace CoursesAPI.Services.Services
{
	public class CoursesServiceProvider
	{
		private readonly IUnitOfWork _uow;

		private readonly IRepository<CourseInstance> _courseInstances;
		private readonly IRepository<TeacherRegistration> _teacherRegistrations;
		private readonly IRepository<CourseTemplate> _courseTemplates; 
		private readonly IRepository<Person> _persons;
        private readonly IRepository<Semester> _semesters;
        private readonly IRepository<Project> _projects;
        private readonly IRepository<Grade> _grades;
        private readonly IRepository<ProjectGroup> _projectGroups;

		public CoursesServiceProvider(IUnitOfWork uow)
		{
			_uow = uow;

			_courseInstances      = _uow.GetRepository<CourseInstance>();
			_courseTemplates      = _uow.GetRepository<CourseTemplate>();
			_teacherRegistrations = _uow.GetRepository<TeacherRegistration>();
			_persons              = _uow.GetRepository<Person>();
            _semesters            = _uow.GetRepository<Semester>();
            _projects             = _uow.GetRepository<Project>();
            _grades               = _uow.GetRepository<Grade>();
            _projectGroups        = _uow.GetRepository<ProjectGroup>();
		}

		public List<Person> GetCourseTeachers(int courseInstanceID)
		{
            // TODO:
            var result = from tr in _teacherRegistrations.All()
                         join p in _persons.All() on tr.SSN equals p.SSN
                         where tr.CourseInstanceID == courseInstanceID
                         select p;

			return result.ToList();
		}

		public List<CourseInstanceDTO> GetCourseInstancesOnSemester(string semester)
		{
            if (String.IsNullOrEmpty(semester))
            {
                semester = _semesters.All().OrderByDescending(x => x.DateBegins).Select(s => s.ID).FirstOrDefault();
            }

            var result = from ci in _courseInstances.All()
                         join c in _courseTemplates.All() on ci.CourseID equals c.CourseID
                         where ci.SemesterID == semester
                         select new CourseInstanceDTO
                         {
                             CourseInstanceID = ci.ID,
                             CourseID = ci.CourseID,
                             Name = c.Name,
                             MainTeacher = "Main teacher Name"
                         };

            return result.OrderBy(c => c.Name).ToList();
		}

		public List<CourseInstanceDTO> GetSemesterCourses(string semester)
		{
            if (String.IsNullOrEmpty(semester))
            {
                semester = _semesters.All().OrderByDescending(x => x.DateBegins).Select(s => s.ID).FirstOrDefault();
            }

            var result = from ci in _courseInstances.All()
                         join c in _courseTemplates.All() on ci.CourseID equals c.CourseID
                         where ci.SemesterID == semester
                         select new CourseInstanceDTO
                         {
                             CourseInstanceID = ci.ID,
                             CourseID = ci.CourseID,
                             Name = c.Name,
                             MainTeacher = "Main teacher Name"
                         };

            return result.OrderBy(c => c.Name).ToList();
		}

        public ProjectGroupDTO AddToProjectGroup(AddProjectGroupViewModel model)
        {
            if (model.Name != null && model.Name.Length < 64)
            {

                var projectGroup = new ProjectGroup
                {
                    Name = model.Name,
                    GradedProjectsCount = model.GradedProjectsCount
                };

                _projectGroups.Add(projectGroup);
                _uow.Save();
                return new ProjectGroupDTO
                {
                    ID = projectGroup.ID,
                    Name = projectGroup.Name,
                    GradedProjectsCount = projectGroup.GradedProjectsCount
                };
            }
            else
            {
                throw new ArgumentException("Name is not legal (required and max length 64)");
            }
            
           
        }

        public ProjectDTO AddProject(int courseInstanceID, int projectGroupID, ProjectViewModel model)
        {
            var courseInstance = _courseInstances.GetCourseInstanceByID(courseInstanceID);

            var projectGroup = (from p in _projectGroups.All()
                               where p.ID == projectGroupID
                               select p).SingleOrDefault();

            if (projectGroup == null) {
                throw new ArgumentException("Project group not found, maybe you have not created it?");
            }

            var project = new Project
                {
                    CourseInstanceID = courseInstance.ID,
                    Name = model.Name,
                    ProjectGroupID = projectGroup.ID,
                    Weight = model.Weight,
                    MinGradeToPassCourse = model.MinGradeToPassCourse,     
                    OnlyIfHigherThanProjectID = model.OnlyHigherThanProjectID
                };

            _projects.Add(project);
            _uow.Save();

            var courseDTO = new CourseInstanceSimpleDTO
            {
                CourseID = courseInstance.CourseID,
                ID = courseInstance.ID,
                Semester = courseInstance.SemesterID

            };

            var projectGroupDTO = new ProjectGroupDTO
            {
                ID = projectGroup.ID,
                Name = projectGroup.Name,
                GradedProjectsCount = projectGroup.GradedProjectsCount
            };

            return new ProjectDTO
            {
                CourseInstance = courseDTO,
                ProjectGroup = projectGroupDTO,
                ID = project.ID,
                MinGradeToPassCourse = project.MinGradeToPassCourse,
                OnlyHigherThanProjectID = project.OnlyIfHigherThanProjectID,
                Weight = project.Weight
            };
        }

        public void AddGradeToProject(int courseInstanceID, int projectID, GradeViewModel model)
        {
            var student = _persons.GetPersonBySSN(model.SSN);
            var course = _courseInstances.GetCourseInstanceByID(courseInstanceID);
            var project = _projects.GetProjectByID(projectID);

            var grade = new Grade
            {
                PersonSSN = student.SSN,
                ProjectGrade = model.Grade,
                ProjectID = project.ID
            };

            _grades.Add(grade);
            _uow.Save();
        }

        public GradeDTO GetGrades(int courseInstanceID, int projectID, string ssn)
        {
            var student = _persons.GetPersonBySSN(ssn);
            var course = _courseInstances.GetCourseInstanceByID(courseInstanceID);
            var project = _projects.GetProjectByID(projectID);
            var projectGroup = _projectGroups.GetProjectGroupByID(project.ProjectGroupID);

            var results = (from g in _grades.All()
                           where g.PersonSSN == student.SSN &&
                           g.ProjectID == project.ID
                           select g).SingleOrDefault();

            var projectDTO = new ProjectDTO
            {
                CourseInstance = new CourseInstanceSimpleDTO
                {
                    CourseID = course.CourseID,
                    ID = course.ID,
                    Semester = course.SemesterID
                },
                OnlyHigherThanProjectID = project.OnlyIfHigherThanProjectID,
                Weight = project.Weight,
                ProjectGroup = new ProjectGroupDTO
                {
                    GradedProjectsCount = projectGroup.GradedProjectsCount,
                    ID = projectGroup.ID,
                    Name = projectGroup.Name
                }

            };

            return new GradeDTO
            {
                ProjectGrade = results.ProjectGrade,
                SSN = student.SSN,
                Project = projectDTO,
                ProjectName = project.Name
            };
        }

        public GradesFromProjectsDTO GetAllGradesFromProjectGroup(int courseInstanceID, int projectGroupID, string ssn)
        {
            var student = _persons.GetPersonBySSN(ssn);
            var course = _courseInstances.GetCourseInstanceByID(courseInstanceID);
            var projectGroup = _projectGroups.GetProjectGroupByID(projectGroupID);

            var projects = (from p in _projects.All()
                            where p.ProjectGroupID == projectGroup.ID
                            select p).ToList();

            var grades = _grades.GetGradesFromStudent(ssn);

            /*
            var selectedProjects = (from r in _projects.All()
                    join k in _grades.All() on r.ID equals k.ProjectID
                    where k.PersonSSN == student.SSN &&
                    r.ProjectGroupID == projectGroup.ID
                    select r).ToList();
             */

            List<GradeDTO> allProjects = new List<GradeDTO>();
            float totalWeight = 0;
            float average = 0;
            int numberOfProjects = 0;
            foreach (Project p in projects)
            {
                foreach (Grade g in grades)
                {
                    if (p.ID == g.ProjectID && p.CourseInstanceID == courseInstanceID)
                    {

                        var shouldAdd = true;
                        if (p.OnlyIfHigherThanProjectID != null && p.MinGradeToPassCourse == null)
                        {
                            var onlyHigherProject = _projects.GetProjectByID((int)p.OnlyIfHigherThanProjectID);
                            var onlyHigherProjectGrade = _grades.GetGradeByProjectID(onlyHigherProject.ID, ssn);
                            if (onlyHigherProjectGrade.ProjectGrade > g.ProjectGrade)
                            {
                                totalWeight += p.Weight;
                                average += onlyHigherProjectGrade.ProjectGrade;
                                shouldAdd = false;

                            }
                            else
                            {
                                shouldAdd = true;
                            }
                        }


                        if (shouldAdd)
                        {
                            totalWeight += p.Weight;
                            average += g.ProjectGrade;
                        }

                        numberOfProjects++;
                        allProjects.Add(new GradeDTO
                        {
                            Project = new ProjectDTO
                            {
                                CourseInstance = new CourseInstanceSimpleDTO
                                {
                                    CourseID = course.CourseID,
                                    ID = course.ID,
                                    Semester = course.SemesterID
                                },
                                ID = p.ID,
                                MinGradeToPassCourse = p.MinGradeToPassCourse,
                                OnlyHigherThanProjectID = p.OnlyIfHigherThanProjectID,
                                ProjectGroup = new ProjectGroupDTO
                                {
                                    GradedProjectsCount = projectGroup.GradedProjectsCount,
                                    ID = projectGroup.ID,
                                    Name = projectGroup.Name
                                },
                                Weight = p.Weight
                            },
                            ProjectGrade = g.ProjectGrade,
                            ProjectName = p.Name,
                            SSN = g.PersonSSN

                        });
                    }
                }
            }



            //X af y bestu gilda utreikningar
            if (projectGroup.GradedProjectsCount != 0)
            {
                allProjects = allProjects.OrderByDescending(g => g.ProjectGrade).Take(projectGroup.GradedProjectsCount).ToList();
                numberOfProjects = 0;
                totalWeight = 0;
                average = 0;
                foreach (GradeDTO g in allProjects)
                {


                    totalWeight += g.Project.Weight;
                    average += g.ProjectGrade;



                    numberOfProjects++;
                }
            }




            average = average / numberOfProjects;

            float aquiredGrade = ((float)totalWeight / 100) * average;

            return new GradesFromProjectsDTO
            {
                Average = average,
                TotalWeight = totalWeight,
                AquiredGrade = aquiredGrade,
                GradeList = allProjects
            };
        }



        public FinalGradeDTO NewGetGradesFromCourse(int courseInstanceID, string ssn)
        {
            var student = _persons.GetPersonBySSN(ssn);
            var course = _courseInstances.GetCourseInstanceByID(courseInstanceID);
            var projectCourses = _projects.GetAllProjectsInCourseByCourseID(courseInstanceID);
            var allGradesFromStudent = _grades.GetGradesFromStudent(ssn);

            var listOfProjectGroupsTaken = new List<int>();
            foreach (Project p in projectCourses)
            {
                listOfProjectGroupsTaken.Add(p.ProjectGroupID);
            }
            listOfProjectGroupsTaken = listOfProjectGroupsTaken.Distinct().ToList();

            var listGradesFromProjects = new List<GradesFromProjectsDTO>();

            foreach (int i in listOfProjectGroupsTaken)
            {
                listGradesFromProjects.Add(GetAllGradesFromProjectGroup(courseInstanceID, i, ssn));
            }

            var didPassExam = true;
            var didPass = true;
            var otherShouldCount = false;
            int? removeThisProject = null;
            float totalGrade = 0;
            foreach (GradesFromProjectsDTO g in listGradesFromProjects)
            {
                foreach (GradeDTO grade in g.GradeList)
                {
                    if (grade.Project.MinGradeToPassCourse != null && grade.ProjectGrade < ((float)grade.Project.MinGradeToPassCourse / 10))
                    {
                        didPassExam = false;
                    }
                    else if (grade.Project.MinGradeToPassCourse != null && grade.ProjectGrade > ((float)grade.Project.MinGradeToPassCourse / 10))
                    {
                        didPassExam = true;
                        removeThisProject = grade.Project.OnlyHigherThanProjectID;
                    }
                    else if (grade.Project.MinGradeToPassCourse == null && grade.Project.OnlyHigherThanProjectID != null)
                    {
                        var project = _projects.GetProjectByID((int)grade.Project.OnlyHigherThanProjectID);
                        var gradeInOther = _grades.GetGradeByProjectID(project.ID, ssn);
                        if (gradeInOther.ProjectGrade > grade.ProjectGrade)
                        {
                            //otherShouldCount = true;
                            //totalGrade -= g.AquiredGrade;
                        }
                        else
                        {
                            //otherShouldCount = false;
                        }
                    }

                }
                if (didPassExam)
                {
                    totalGrade += g.AquiredGrade;
                }



            }

            if (totalGrade > 10)
            {
                totalGrade = 10;
            }

            if (totalGrade < 4.75)
            {
                didPass = false;
            }

            double d = Math.Round(totalGrade * 2) / 2;

            float newValue = Convert.ToSingle(d);

            return new FinalGradeDTO
            {
                DidPass = didPass,
                DidPassExam = didPassExam,
                GradesList = listGradesFromProjects,
                FinalGrade = newValue


            };
        }

        public FinalGradeDTO GetGradesFromCourse(int courseInstanceID, string ssn)
        {
            var student = _persons.GetPersonBySSN(ssn);
            var course = _courseInstances.GetCourseInstanceByID(courseInstanceID);

            var projectCourses = _projects.GetAllProjectsInCourseByCourseID(courseInstanceID);

            var grades = _grades.GetGradesFromStudent(ssn);

            var gradesFromProject = new Dictionary<Project, Grade>();
            var failedProjects = new Dictionary<Project, Grade>();
            var gradeDTO = new List<GradeDTO>();

            float totalGrade = 0;
            bool didPass = true;
 
            foreach (Project p in projectCourses)
            {
                foreach(Grade g in grades) {
                    if (g.ProjectID == p.ID)
                    {
                        //student has a grade in this project!                        
                        if (p.MinGradeToPassCourse != null && ((float)p.MinGradeToPassCourse / 10) > g.ProjectGrade)
                        {
                            didPass = false;
                            failedProjects.Add(p, g);
                        }
                        else if (p.OnlyIfHigherThanProjectID != null) {
   
                            foreach (KeyValuePair<Project, Grade> entry in failedProjects)
                            {
                                if (entry.Key.ID == p.OnlyIfHigherThanProjectID)
                                {
                                    if (g.ProjectGrade > entry.Value.ProjectGrade && g.ProjectGrade > ((float)p.MinGradeToPassCourse/10))
                                    {
                                        didPass = true;
                                        totalGrade += g.ProjectGrade * ((float)p.Weight / 100);
                                        var projectGroup = _projectGroups.GetProjectGroupByID(p.ProjectGroupID);
                                        gradeDTO.Add(new GradeDTO
                                        {
                                            Project = new ProjectDTO
                                            {
                                                CourseInstance = new CourseInstanceSimpleDTO
                                                {
                                                    CourseID = course.CourseID,
                                                    ID = course.ID,
                                                    Semester = course.SemesterID
                                                },
                                                ID = p.ID,
                                                MinGradeToPassCourse = p.MinGradeToPassCourse,
                                                OnlyHigherThanProjectID = p.OnlyIfHigherThanProjectID,
                                                ProjectGroup = new ProjectGroupDTO
                                                {
                                                    GradedProjectsCount = projectGroup.GradedProjectsCount,
                                                    ID = projectGroup.ID,
                                                    Name = projectGroup.Name
                                                },
                                                Weight = p.Weight
                                            },
                                            ProjectGrade = g.ProjectGrade,
                                            ProjectName = p.Name,
                                            SSN = student.SSN,                                            
                                        });
                                    }
                                    
                                }
                            }
                        }
                        else
                        {
                            gradesFromProject.Add(p, g);
                            totalGrade += g.ProjectGrade * ((float)p.Weight / 100);
                            var projectGroup = _projectGroups.GetProjectGroupByID(p.ProjectGroupID);
                            gradeDTO.Add(new GradeDTO
                                        {
                                            Project = new ProjectDTO
                                            {
                                                CourseInstance = new CourseInstanceSimpleDTO
                                                {
                                                    CourseID = course.CourseID,
                                                    ID = course.ID,
                                                    Semester = course.SemesterID
                                                },
                                                ID = p.ID,
                                                MinGradeToPassCourse = p.MinGradeToPassCourse,
                                                OnlyHigherThanProjectID = p.OnlyIfHigherThanProjectID,
                                                ProjectGroup = new ProjectGroupDTO
                                                {
                                                    GradedProjectsCount = projectGroup.GradedProjectsCount,
                                                    ID = projectGroup.ID,
                                                    Name = projectGroup.Name
                                                },
                                                Weight = p.Weight
                                            },
                                            ProjectGrade = g.ProjectGrade,
                                            ProjectName = p.Name,
                                            SSN = student.SSN,                                            
                                        });
                        }                                                                        
                        
                    }
                }
                
            }

            if (totalGrade > 10)
            {
                totalGrade = 10;
            }

            if (totalGrade < 4.75)
            {
                didPass = false;
            }

            double d = Math.Round(totalGrade * 2) / 2;           

            float newValue = Convert.ToSingle(d);
                    
            return new FinalGradeDTO
            {
                DidPass = didPass,
                FinalGrade = newValue,
                PlaceInStudents = 0                
            };
            
        }
    }
}
