using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using cdod.Services;

namespace cdod.Models
{
    public class StudentToCourse
    {
        [IsProjected]
        public int StudentId { get; set; }

        public Student Student { get; set; }

        [IsProjected]
        public int CourseId { get; set; }

        public Course Course { get; set; }

        public DateOnly SignDate { get; set; }

        [IsProjected]
        public int ContractStateId { get; set; }

        public ContractState ContractState { get; set; }

        public string? ContractUrl { get; set; }

        public bool? EquipmentPriceWithRobot { get; set; }

        //Default value = 0
        public double? Debt { get; set; }
    }
}
