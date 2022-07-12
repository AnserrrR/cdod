using cdod.Models;
using cdod.Services;
using cdod.Services.DataLoaders;
using cdod.Services.HelperTypes;
using Microsoft.EntityFrameworkCore;

namespace cdod.Schema.OutputTypes
{
    public class InfoType
    {
        [IsProjected]
        public int CourseId { get; set; }

        [IsProjected]
        public int StudentId { get; set; }

        public DateOnly AdmissionDate { get; set; }

        public bool? IsGetRobot { get; set; }

        [IsProjected]
        public int ContractStateId { get; set; }

        public async Task<string> ContractState([Service] ContractStateDataLoader contractStateDataLoader)
        {
            var contractState = await contractStateDataLoader.LoadAsync(ContractStateId);
            return contractState.Name;
        }

        [UseProjection]
        public async Task<Course> Course([Service] CourseDataLoader courseDataLoader)
        {
            var course = await courseDataLoader.LoadAsync(CourseId);
            return course;
        }

        [UseProjection]
        public async Task<Group?> Group([Service] GroupByStudentIdCourseIdDataLoader groupByStudentIdCourseIdDataLoader)
        {
            var scg = await groupByStudentIdCourseIdDataLoader.LoadAsync((CourseId, StudentId) );
            return scg?.Group;
        }

        public async Task<bool> IsCoursePaid([Service] PayInfoDataLoader payInfoDataLoader,
            [Service] PayNotesDataLoader payNotesDataLoader)
        {
            return await IsPaid(Appointment.Course, payInfoDataLoader, payNotesDataLoader) ?? false;
        }

        public async Task<bool?> IsEquipmentPaid([Service] PayInfoDataLoader payInfoDataLoader,
            [Service] PayNotesDataLoader payNotesDataLoader)
        {

            try
            {
                return await IsPaid(Appointment.Material, payInfoDataLoader, payNotesDataLoader);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception(e.Message);
            }
        }

        private async Task<bool?> IsPaid(Appointment appointment, 
            [Service] PayInfoDataLoader payInfoDataLoader, [Service]PayNotesDataLoader payNotesDataLoader)
        {

            var payInfo = await payInfoDataLoader.LoadAsync((CourseId,StudentId));

            if (appointment == Appointment.Material && payInfo.CourseName != "Робофабрика") return null;

            if (payInfo.ContractState == "Зачислен")
            {
                {
                    var totalPrice = payInfo.CoursePrice;

                    if (appointment == Appointment.Material)
                    {
                        if (payInfo.IsEquipmentPriceWithRobot == true)
                            totalPrice = payInfo.EquipmentPriceWithRobot ?? 0;
                        else if (payInfo.IsEquipmentPriceWithRobot == false)
                            totalPrice = payInfo.EquipmentPriceWithoutRobot ?? 0;
                        else
                            throw new Exception("Not enough information: will the student pick up the robot");
                    }

                    var totalPayment = await payNotesDataLoader.LoadAsync((CourseId, StudentId, appointment));

                    var monthPassed = (DateTime.Today.Year - payInfo.SignDate.Year) * 12 + DateTime.Today.Month - payInfo.SignDate.Month;

                    if (monthPassed < payInfo.DurationInMonths / 2) totalPrice /= 2;

                    return totalPrice <= totalPayment?.TotalSum;
                }
            }
            return false;
        }
    }
}
