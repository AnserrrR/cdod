using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace cdodDTOs.DTOs
{
    public class LessonDTO
    {
        [Key]
        public int Id { get; set; }

        public string Homework { get; set; }

        public TimeOnly StartTime { get; set; }

        public int Duration { get; set; }

        public string ClassRoom { get; set; }

        public string LessonTopic { get; set; }

        public int GroupId { get; set; }

        public GroupDTO Group { get; set; }

        public IEnumerable<TeacherDTO?> Teachers { get; set; } = new List<TeacherDTO?>();

        public IEnumerable<StudentDTO?> Students { get; set; } = new List<StudentDTO?>();

        public IEnumerable<TeacherToLessonDTO?> TeacherToLessons { get; set; } = new List<TeacherToLessonDTO?>();

        public IEnumerable<StudentToLessonDTO?> StudentsToLessons { get; set; } = new List<StudentToLessonDTO?>();
    }
}
