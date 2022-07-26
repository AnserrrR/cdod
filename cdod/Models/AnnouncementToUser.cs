namespace cdod.Models
{
    public class AnnouncementToUser
    {
        public int AnnouncementId { get; set; }

        public Announcement Announcement { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
