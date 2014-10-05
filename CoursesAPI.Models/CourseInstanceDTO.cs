namespace CoursesAPI.Models
{
	/// <summary>
	/// A DTO class for the Course Instance Entity Class
	/// </summary>
	public class CourseInstanceDTO
	{
        /// <summary>
        /// Database generated ID of the Course instance
        /// </summary>
		public int    CourseInstanceID { get; set; }

        /// <summary>
        /// String represenatation of the course (ex. T-123-TEST)
        /// </summary>
		public string CourseID         { get; set; }

        /// <summary>
        /// Name of the course (ex. Vefforritun)
        /// </summary>
		public string Name             { get; set; }

        /// <summary>
        /// Main teacher of the course
        /// </summary>
		public string MainTeacher      { get; set; }
	}
}
