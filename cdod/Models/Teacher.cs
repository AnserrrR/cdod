using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cdod.Models
{
    public class Teacher
    {
        [Key]
        [ForeignKey("User")]
        public int UserId { get; set; }

        public User User { get; set; }

        public string WorkPlace { get; set; }

        public int PostId { get; set; }

        public Post Post { get; set; }

        public int WageRateId { get; set; }

        public WageRate WageRate { get; set; }

        public IEnumerable<TeacherToLesson?> TeacherToLessons { get; set; } = new List<TeacherToLesson?>();

        public IEnumerable<Course?> Courses { get; set; } = new List<Course?>();
        public IEnumerable<Group?> Groups { get; set; } = new List<Group?>();
    }
}
