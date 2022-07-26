using System.ComponentModel.DataAnnotations;

namespace cdod.Models
{
    public class Group
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int TeacherId { get; set; }

        public Teacher Teacher { get; set; }

        public DateOnly StartDate { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }

        public IEnumerable<Lesson?> Lessons { get; set; } = new List<Lesson?>();

        public IEnumerable<Announcement?> Announcements { get; set; } = new List<Announcement?>();

        //public IEnumerable<Student?> Students { get; set; } = new List<Student?>();
        public IEnumerable<StudentToCourse?> StudentsToGroups { get; set; } = new List<StudentToCourse?>();
    }
}
