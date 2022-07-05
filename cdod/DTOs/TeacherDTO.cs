using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cdodDTOs.DTOs
{
    public class TeacherDTO
    {
        [Key]
        [ForeignKey("User")]
        public int UserId { get; set; }

        public UserDTO User { get; set; }

        public string WorkPlace { get; set; }

        public int PostId { get; set; }

        public PostDTO Post { get; set; }

        public IEnumerable<LessonDTO> Lessons { get; set; }
        public IEnumerable<TeacherToLessonDTO> TeacherToLessons { get; set; }

        public IEnumerable<CourseDTO> Courses { get; set; }
        public IEnumerable<GroupDTO> Groups { get; set; }
    }
}
