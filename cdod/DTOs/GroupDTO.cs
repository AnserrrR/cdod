using System.ComponentModel.DataAnnotations;

namespace cdodDTOs.DTOs
{
    public class GroupDTO
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int TeacherId { get; set; }

        public TeacherDTO Teacher { get; set; }

        public int StartYear { get; set; }

        public int CourseId { get; set; }

        public CourseDTO Course { get; set; }

        public IEnumerable<LessonDTO> Lessons { get; set; }
        public IEnumerable<AnnouncementDTO> Announcements { get; set; }
        public IEnumerable<StudentDTO> Students { get; set; }
    }
}
