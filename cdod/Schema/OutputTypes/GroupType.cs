using System.ComponentModel.DataAnnotations;
using cdod.Models;

namespace cdod.Schema.OutputTypes
{
    public class GroupType
    {
        [IsProjected]
        public int Id { get; set; }

        public string Name { get; set; }

        [IsProjected]
        public int TeacherId { get; set; }

        public Teacher Teacher { get; set; }

        public DateOnly StartDate { get; set; }

        [IsProjected]
        public int CourseId { get; set; }

        public Course Course { get; set; }

        public IEnumerable<Lesson?> Lessons { get; set; } = new List<Lesson?>();

        public IEnumerable<Announcement?> Announcements { get; set; } = new List<Announcement?>();

        public IEnumerable<StudentType?> Students { get; set; } = new List<StudentType?>();
    }
}
