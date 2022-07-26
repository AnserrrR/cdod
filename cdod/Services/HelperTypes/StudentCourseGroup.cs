using cdod.Models;

namespace cdod.Services.HelperTypes
{
    public class StudentCourseGroup
    {
        public int StudentId { get; set; }

        public int CourseId { get; set; }

        public int Attempt { get; set; }

        public int? GroupId { get; set; }

        public Group Group { get; set; }
    }
}
