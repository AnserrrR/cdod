using System.ComponentModel.DataAnnotations;

namespace cdod.Models
{
    public enum AnnouncementTypeEnum
    {
        important,
        unimportant
    }

    public class Announcement
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

        public User UserCreator { get; set; }

        public IEnumerable<Course?> Courses { get; set; } = new List<Course?>();
        public IEnumerable<Group?> Groups { get; set; } = new List<Group?>();
        public IEnumerable<AnnouncementToUser?> AnnouncementToUsers { get; set; } = new List<AnnouncementToUser?>();
    }
}
