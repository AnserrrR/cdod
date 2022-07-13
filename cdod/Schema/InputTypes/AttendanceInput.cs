using cdod.Models;

namespace cdod.Schema.InputTypes
{
    public class StudentAttendanceInput
    {
        public int studentId { get; set; }
        public int lessonId { get; set; }
        public int? mark { get; set; }
        public string? note { get; set; }
        public Status? status { get; set; } // Добавить в дефолт 0 
    }

    public class TeacherAttendanceInput
    {
        public int teacherId { get; set; }
        public int lessonId { get; set; }
        public TimeOnly workTime { get; set; }
    }
}
