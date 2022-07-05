using System.ComponentModel.DataAnnotations;

namespace cdodDTOs.DTOs
{
    public class StudentDTO
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Patronymic { get; set; }
        public string Descriotion { get; set; }
        public DateOnly BirthDate { get; set; }

        public int SchoolId { get; set; }
        public SchoolDTO School { get; set; }

        public int ParentId { get; set; }
        public ParentDTO Parent { get; set; }

        public IEnumerable<StudentToCourseDTO> StudentToCourses { get; set; }
        public IEnumerable<StudentToLessonDTO> StudentToLessons { get; set; }

        public IEnumerable<CourseDTO> Courses { get; set; }
        public IEnumerable<LessonDTO> Lessons { get; set; }
        public IEnumerable<GroupDTO> Groups { get; set; }
        public IEnumerable<PayNoteDTO> PayNotes { get; set; }
    }
}
