using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

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

        public bool IsCoursePaid()
        {
            return IsPaid(Appointment.Course);
        }

        public bool? IsEquipmentPaid()
        {
            if (Course.Name != "Робототехника") return null;
            return IsPaid(Appointment.Material);
        }

        private bool IsPaid(Appointment appointment)
        {
            if (ContractState?.Name == "Зачислен")
            {
                var MonthCost = appointment == Appointment.Course ? (Course.CoursePrice / Course.DurationInMonths) : Course.EquipmentPrice;
                var MonthPayment = Course.PayNotes.FirstOrDefault(pn =>
                {
                    return (pn.Course.Id == CourseId) && (pn.Date.Month == DateTime.Today.Month) && (pn.Appointment == appointment);
                })?.Sum ?? 0;

                return MonthCost <= MonthPayment;
            }
            return false;
        }
    }
}
