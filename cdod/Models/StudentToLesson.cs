using System.ComponentModel.DataAnnotations;

namespace cdods.s
{
    public class StudentToLesson
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }

        public int Mark { get; set; }

        public string Note { get; set; }

        public string Status { get; set; }
    }
}
