using cdod.Models;
using cdod.Services;
using Microsoft.EntityFrameworkCore;

namespace cdod.Schema.OutputTypes
{
    public class InfoType
    {

        [IsProjected]
        public int CourseId { get; set; }

        [IsProjected]
        public int StudentId { get; set; }

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<Course> Course([ScopedService] CdodDbContext context)
        {
            Course course = await context.Courses.FindAsync(CourseId);
            return course;
        }

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<Group> Group([ScopedService] CdodDbContext context)
        {
            var group = await context.Groups.FirstOrDefaultAsync(g => (g.CourseId == CourseId)
                                                                        && (g.Students.Select(s => s.Id)
                                                                            .Contains(StudentId)));
            return group ?? new Group();
        }

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<bool> IsCoursePaid([ScopedService] CdodDbContext context)
        {
            return await IsPaid(Appointment.Course, context) ?? false;
        }

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<bool?> IsEquipmentPaid([ScopedService] CdodDbContext context)
        {

            try
            {
                return await IsPaid(Appointment.Material, context);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception(e.Message);
            }
        }

        [UseDbContext(typeof(CdodDbContext))]
        private async Task<bool?> IsPaid(Appointment appointment, [ScopedService] CdodDbContext ctx)
        {
            var payInfoQuery =
                from stc in ctx.StudentToCourses
                join c in ctx.Courses on stc.CourseId equals c.Id
                join cs in ctx.ContractStates on stc.ContractStateId equals cs.Id
                //join pn in ctx.PayNotes on stc.CourseId equals pn.CourseId into pns
                where (stc.CourseId == CourseId) && (stc.StudentId == StudentId)
                select new
                {
                    CourseId = stc.CourseId,
                    StudentId = stc.StudentId,
                    ContractState = cs.Name,
                    CoursePrice = c.CoursePrice,
                    CourseName = c.Name,
                    DurationInMonths = c.DurationInMonths,
                    SignDate = stc.SignDate,
                    IsEquipmentPriceWithRobot = stc.EquipmentPriceWithRobot,
                    EquipmentPriceWithRobot = c.EquipmentPriceWithRobot,
                    EquipmentPriceWithoutRobot = c.EquipmentPriceWithoutRobot,
                    //PayNotes = pns
                };

            var payInfo = payInfoQuery.FirstOrDefault();

           

            if (payInfo.ContractState == "Зачислен")
            {
                {
                    var totalPrice = payInfo.CoursePrice;

                    if (appointment == Appointment.Material)
                    {
                        if (payInfo.CourseName != "Робототехника") return null;

                        if (payInfo.IsEquipmentPriceWithRobot == true)
                            totalPrice = payInfo.EquipmentPriceWithRobot ?? 0;
                        else if (payInfo.IsEquipmentPriceWithRobot == false)
                            totalPrice = payInfo.EquipmentPriceWithoutRobot ?? 0;
                        else
                            throw new Exception("Not enough information: will the student pick up the robot");
                    }

                    var totalPayment = await ctx.PayNotes.Where(pn =>
                        (pn.Appointment == appointment) && (pn.CourseId == CourseId) && (pn.StudentId == StudentId)
                    ).SumAsync(pn => pn.Sum);

                    var monthPassed = (DateTime.Today.Year - payInfo.SignDate.Year) * 12 + DateTime.Today.Month - payInfo.SignDate.Month;

                    if (monthPassed < payInfo.DurationInMonths / 2) totalPrice /= 2;

                    return totalPrice <= totalPayment;
                }
            }
            return false;
        }
    }
}
