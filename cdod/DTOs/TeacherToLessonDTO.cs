using System.ComponentModel.DataAnnotations;

namespace cdodDTOs.DTOs
{
    public class TeacherToLessonDTO
    {
        public int TeacherId { get; set; }
        public TeacherDTO Teacher { get; set; }

        public int LessonId { get; set; }
        public LessonDTO Lesson { get; set; }

        public TimeOnly WorkTime { get; set; }
    }
}
