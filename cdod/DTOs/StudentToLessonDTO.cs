using System.ComponentModel.DataAnnotations;

namespace cdodDTOs.DTOs
{
    public class StudentToLessonDTO
    {
        public int StudentId { get; set; }
        public StudentDTO Student { get; set; }

        public int LessonId { get; set; }
        public LessonDTO Lesson { get; set; }

        public int Mark { get; set; }

        public string Note { get; set; }

        public string Status { get; set; }
    }
}
