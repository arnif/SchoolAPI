using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoursesAPI.Services.DataAccess;
using CoursesAPI.Services.Models.Entities;

namespace CoursesAPI.Services.Extensions
{
    public static class GradesExtensions
    {
        public static List<Grade> GetGradesFromStudent(this IRepository<Grade> repo, string ssn)
        {
            var grades = (from g in repo.All()
                          where g.PersonSSN == ssn
                          select g).ToList();

            if (grades == null)
            {
                throw new ArgumentException("Grades not found, please try again.");
            }

            return grades;
        }

        public static Grade GetGradeByProjectID(this IRepository<Grade> repo, int id, string ssn)
        {
            var grade = repo.All().SingleOrDefault(g => g.ProjectID == id && g.PersonSSN == ssn);

            if (grade == null)
            {
                throw new ArgumentException("Grades not found, please try again.");
            }

            return grade;

        }
    }
}
