using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using cdod.Services;

namespace cdod.Models
{
    public enum ContractState
    {
        Consideration,
        Rejected,
        Studying,     
        Completed,   
        Left,         
        Excluded     
    }

    public class StudentToCourse
    {
        public int StudentId { get; set; }

        public Student Student { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }

        public int Attempt { get; set; }

        public DateOnly SignDate { get; set; }

        public ContractState ContractState { get; set; }

        public string? ContractUrl { get; set; }

        public bool? IsGetRobot { get; set; }

        public int? GroupId { get; set; }

        public Group? Group { get; set; }

        public IEnumerable<PayNote?>? PayNotes { get; set; }

        //Default value = 0
        public double? Debt { get; set; }
    }
}
