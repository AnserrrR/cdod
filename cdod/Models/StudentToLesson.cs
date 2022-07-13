using System.ComponentModel.DataAnnotations;

namespace cdod.Models
{
    public enum Status
    {
        was,
        dontwas
    };
    public class StudentToLesson
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }

        public int Mark { get; set; }

        public string Note { get; set; }

        public Status Status { get; set; }
    }
}
