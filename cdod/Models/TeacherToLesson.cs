using System.ComponentModel.DataAnnotations;

namespace cdods.s
{
    public class TeacherToLesson
    {
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }

        public TimeOnly WorkTime { get; set; }
    }
}
