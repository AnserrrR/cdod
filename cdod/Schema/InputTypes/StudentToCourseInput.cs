using cdod.Models;

namespace cdod.Schema.InputTypes
{
    public class StudentToCourseCreateInput
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int? GroupId { get; set; }

        public DateOnly? AdmissionDate { get; set; }

        public ContractState ContractState { get; set; }

/*        [GraphQLType(typeof(UploadType))]
        public IFile? Contract { get; set; }*/
        public string? ContractUrl { get; set; }

        public bool? IsGetRobot { get; set; }
    }

    public class StudentToCourseUpdateInput
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int Attempt { get; set; }
        public int GroupId { get; set; }

        public DateOnly? AdmissionDate { get; set; }

        public ContractState? ContractState { get; set; }

        public string? ContractUrl { get; set; }

        public bool? IsGetRobot { get; set; }
    }

    public class StudentToCourseDetachInput
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int Attempt { get; set; }
    }
}
