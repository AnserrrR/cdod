using cdod.Models;

namespace cdod.Schema.InputTypes
{
    public class StudentAttendanceInput
    {
        public int StudentId { get; set; }
        public int LessonId { get; set; }
        public int? Mark { get; set; }
        public string? Note { get; set; }
        public Status? Status { get; set; } // Добавить в дефолт 0 
    }

    public class TeacherAttendanceInput
    {
        public int TeacherId { get; set; }
        public int LessonId { get; set; }
        public TimeOnly WorkTime { get; set; }
    }
}
