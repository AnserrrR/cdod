using cdod.Models;

namespace cdod.Schema.InputTypes
{
    public class StudentToCourseCreateInput
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }

        public DateOnly admissionDate { get; set; }

        public ContractState ContractState { get; set; }

        public string? ContractUrl { get; set; }

        public bool? isGetRobot { get; set; }
    }

    public class StudentToCourseUpdateInput
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }

        public DateOnly? admissionDate { get; set; }

        public ContractState? ContractState { get; set; }

        public string? ContractUrl { get; set; }

        public bool? isGetRobot { get; set; }
    }

    public class StudentToCourseDetach
    {
        public int studentId { get; set; }
        public int courseId { get; set; }
    }
}
