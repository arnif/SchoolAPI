namespace CoursesAPI.Services.Models.Entities
{
    public class CourseStudent
    {
        /// <summary>
        /// A database-generated ID for the given record.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// The SSSN of the student
        /// </summary>
        public string SSN { get; set; }

        /// <summary>
        /// The database-generated ID if the course instance.
        /// </summary>
        public int CourseInstanceID { get; set; }

        /// <summary>
        /// The status of the student:
        /// 1 : regirsterd/active
        /// 2: quit
        /// </summary>
        public int Status { get; set; }
    }
}
