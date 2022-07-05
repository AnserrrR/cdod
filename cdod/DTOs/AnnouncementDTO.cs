using System.ComponentModel.DataAnnotations;

namespace cdodDTOs.DTOs
{
    public enum AnnouncementTypeEnum
    {
        important,
        unimportant
    }

    public class AnnouncementDTO
    {
        [Key]
        public int Id { get; set; } 

        public string Text { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public string Heading { get; set; }

        public AnnouncementTypeEnum Type { get; set; }

        public string PictureUrl { get; set; }

        public int UserId { get; set; }

        public UserDTO User { get; set; }

        public IEnumerable<CourseDTO> Courses { get; set; }
        public IEnumerable<GroupDTO> Groups { get; set; }
    }
}
