using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace cdod.Models
{
    public class Lesson
    {
        [Key]
        public int Id { get; set; }

        public string Homework { get; set; }

        public TimeOnly StartTime { get; set; }

        public int Duration { get; set; }

        public string ClassRoom { get; set; }

        public string LessonTopic { get; set; }

        public int GroupId { get; set; }

        public Group Group { get; set; }

        //public IEnumerable<Teacher?> Teachers { get; set; } = new List<Teacher?>();

        //public IEnumerable<Student?> Students { get; set; } = new List<Student?>();

        public IEnumerable<TeacherToLesson?> TeacherToLessons { get; set; } = new List<TeacherToLesson?>();

        public IEnumerable<StudentToLesson?> StudentsToLessons { get; set; } = new List<StudentToLesson?>();
    }
}
