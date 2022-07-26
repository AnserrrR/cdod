using System.ComponentModel.DataAnnotations;

namespace cdod.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Patronymic { get; set; }
        public string? Description { get; set; }
        public DateOnly? BirthDate { get; set; }

        public int? SchoolId { get; set; }
        public School? School { get; set; }

        public int ParentId { get; set; }
        public Parent Parent { get; set; }

        public IEnumerable<StudentToCourse?> StudentToCourses { get; set; } = new List<StudentToCourse?>();
        public IEnumerable<StudentToLesson?> StudentToLessons { get; set; } = new List<StudentToLesson?>();

        //public IEnumerable<Course?> Courses { get; set; } = new List<Course?>();
        //public IEnumerable<Lesson?> Lessons { get; set; } = new List<Lesson?>();
        //public IEnumerable<Group?> Groups { get; set; } = new List<Group?>();
        public IEnumerable<PayNote?> PayNotes { get; set; } = new List<PayNote?>();
    }
}
