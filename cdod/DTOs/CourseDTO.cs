using System.ComponentModel.DataAnnotations;

namespace cdodDTOs.DTOs
{
    public class CourseDTO
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string ProgramFileUrl { get; set; }

        public decimal CoursePrice { get; set; }

        public decimal EquipmentPrice { get; set; }

        public IEnumerable<StudentDTO?> Students { get; set; } = new List<StudentDTO?>();
        public IEnumerable<StudentToCourseDTO?> StudentToCourses { get; set; } = new List<StudentToCourseDTO?>();

        public IEnumerable<AnnouncementDTO?> Announcements { get; set; } = new List<AnnouncementDTO?>();
        public IEnumerable<TeacherDTO?> Teachers { get; set; } = new List<TeacherDTO?>();
        public IEnumerable<GroupDTO?> Groups { get; set; } = new List<GroupDTO?>();
    }
}
