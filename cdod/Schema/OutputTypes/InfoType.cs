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

        [IsProjected]
        public int Attempt { get; set; }

        public DateOnly AdmissionDate { get; set; }

        public bool? IsGetRobot { get; set; }

        public ContractState ContractState { get; set; }

        [UseProjection]
        public async Task<CourseType> Course([Service] CourseDataLoader courseDataLoader)
        {
            var course = await courseDataLoader.LoadAsync(CourseId);
            return new CourseType()
            {
                Id = course.Id,
                Name = course.Name,
                ProgramId = course.ProgramId,
                CoursePrice = course.Price,
                EquipmentPriceWithRobot = course.EquipmentPriceWithRobot,
                EquipmentPriceWithoutRobot = course.EquipmentPriceWithoutRobot,
                DurationInMonths = course.DurationInMonths
            };
        }

        [UseProjection]
        public async Task<GroupType?> Group([Service] GroupByStudentIdCourseIdDataLoader groupByStudentIdCourseIdDataLoader)
        {
            var scg = await groupByStudentIdCourseIdDataLoader.LoadAsync((CourseId, StudentId, Attempt) );

            if (scg?.GroupId is int groupId)
            {
                return new GroupType()
                {
                    Id = groupId,
                    Name = scg.Group.Name,
                    StartDate = scg.Group.StartDate,
                    CourseId = scg.Group.CourseId,
                    TeacherId = scg.Group.TeacherId
                };
            }
            return null;
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
                throw new GraphQLException(e.Message);
            }
        }

        private async Task<bool?> IsPaid(Appointment appointment, 
            [Service] PayInfoDataLoader payInfoDataLoader, [Service]PayNotesDataLoader payNotesDataLoader)
        {

            var payInfo = await payInfoDataLoader.LoadAsync((CourseId,StudentId, Attempt));

            if (appointment == Appointment.Material && payInfo.CourseName != "Робофабрика") return null;

            //Номер попытки не важен
            if (payInfo.ContractState == ContractState.Studying)
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

                    var totalPayment = await payNotesDataLoader.LoadAsync((CourseId, StudentId, Attempt, appointment));

                    var monthPassed = (DateTime.Today.Year - payInfo.SignDate.Year) * 12 + DateTime.Today.Month - payInfo.SignDate.Month;

                    if (monthPassed < payInfo.DurationInMonths / 2) totalPrice /= 2;

                    return totalPrice <= totalPayment?.TotalSum;
                }
            }
            return false;
        }
    }
}
