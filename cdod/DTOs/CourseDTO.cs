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

        public IEnumerable<StudentDTO> Students { get; set; }
        public IEnumerable<StudentToCourseDTO> StudentToCourses { get; set; }

        public IEnumerable<AnnouncementDTO> Announcements { get; set; }
        public IEnumerable<TeacherDTO> Teachers { get; set; }
        public IEnumerable<GroupDTO> Groups { get; set; }
    }
}
