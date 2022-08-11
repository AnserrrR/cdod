using cdod.Models;

namespace cdod.Services.HelperTypes
{
    public class PayInfo
    {
        public int CourseId { get; set; }

        public int StudentId { get; set; }

        public int Attempt { get; set; }

        public ContractState ContractState { get; set; }

        public double? CoursePrice { get; set; }

        public string CourseName { get; set; }

        public int? DurationInMonths { get; set; }

        public DateOnly SignDate { get; set; }

        public bool? IsEquipmentPriceWithRobot { get; set; }

        public double? EquipmentPriceWithRobot { get; set; }
        
        public double? EquipmentPriceWithoutRobot { get; set; }
    }
}
