using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using cdod.Services;

namespace cdods.s
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

        public string ContractUrl { get; set; }

        public bool? EquipmentPriceWithRobot { get; set; }

        //Default value = 0
        public double Debt { get; set; }

        public bool IsCoursePaid()
        {

            return IsPaid(Appointment.Course);
        }

        public bool? IsEquipmentPaid()
        {

            if (Course.Name != "Робототехника") return null;

            try
            {
                return IsPaid(Appointment.Material);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception(e.Message);
            }
        }

        private bool IsPaid(Appointment appointment)
        {
            if (ContractState?.Name == "Зачислен")
            {
                var totalPrice = Course.CoursePrice;

                if (appointment == Appointment.Material)
                {
                    if (EquipmentPriceWithRobot == true)
                        totalPrice = Course.EquipmentPriceWithRobot ?? 0;
                    else if (EquipmentPriceWithRobot == false)
                        totalPrice = Course.EquipmentPriceWithoutRobot ?? 0;
                    else
                        throw new Exception("Not enough information: will the student pick up the robot");
                }

                var totalPayment = Course.PayNotes.Where(pn =>
                {
                    return (pn.CourseId == CourseId) && (pn.Appointment == appointment);
                }).Sum(pn => pn.Sum);

                var monthPassed = (DateTime.Today.Year - SignDate.Year) * 12 + DateTime.Today.Month - SignDate.Month;

                if (monthPassed < Course.DurationInMonths / 2) totalPrice /= 2;

                return totalPrice <= totalPayment;
            }
            return false;
        }

        //[UseDbContext(typeof(CdodDbContext))]
        //private void EmptyFieldsHandler([ScopedService] CdodDbContext context)
        //{
        //    if (Course is null) Course = context.Courses.FirstOrDefault(c => c.Id == CourseId);

        //    if (ContractState is null) ContractState = context.ContractStates.FirstOrDefault(cs => cs.Id == ContractStateId);

        //    if (Course.PayNotes is null) context.Courses.FirstOrDefault(cs => cs.Id == ContractStateId);
        //}
    }
}
