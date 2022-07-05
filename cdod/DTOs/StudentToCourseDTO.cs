using System.ComponentModel.DataAnnotations;

namespace cdodDTOs.DTOs
{
    public class StudentToCourseDTO
    {
        public int StudentId { get; set; }
        public StudentDTO Student { get; set; }

        public int CourseId { get; set; }
        public CourseDTO Course { get; set; }

        public int SignYear { get; set; }

        public int ContractStateId { get; set; }
        public ContractStateDTO ContractState { get; set; }
    }
}
