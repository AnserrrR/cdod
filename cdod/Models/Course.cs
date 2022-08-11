using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace cdod.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public double? CoursePrice { get; set; }

        public double? EquipmentPriceWithRobot { get; set; }

        public double? EquipmentPriceWithoutRobot { get; set; }

        public int? DurationInMonths { get; set; }

        public string? Color { get; set; }

        public string? SvgIconUrl { get; set; }

        public int? ProgramId { get; set; }
        public CourseProgram? Program { get; set; }

        public IEnumerable<StudentToCourse?> StudentToCourses { get; set; } = new List<StudentToCourse?>();
        public IEnumerable<PayNote> PayNotes { get; set; } = new List<PayNote>();
        public IEnumerable<Announcement?> Announcements { get; set; } = new List<Announcement?>();
        public IEnumerable<Teacher?> Teachers { get; set; } = new List<Teacher?>();
        public IEnumerable<Group?> Groups { get; set; } = new List<Group?>();
    }
}
