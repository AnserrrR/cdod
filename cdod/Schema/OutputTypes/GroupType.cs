using System.ComponentModel.DataAnnotations;
using cdod.Models;
using cdod.Services.DataLoaders;

namespace cdod.Schema.OutputTypes
{
    public class GroupType
    {
        [IsProjected]
        public int Id { get; set; }

        public string Name { get; set; }

        [IsProjected]
        public int TeacherId { get; set; }

        public async Task<TeacherType> Teacher([Service] TeacherDataLoader teacherDataLoader,
            [Service]UserDataLoader userDataLoader)
        {
            var teacher = await teacherDataLoader.LoadAsync(TeacherId);

            return new TeacherType(await userDataLoader.LoadAsync(TeacherId))
            {
                Id = teacher.UserId,
                PostId = teacher.PostId,
                WorkPlace = teacher.WorkPlace
            };
        }

        public DateOnly StartDate { get; set; }

        [IsProjected]
        public int CourseId { get; set; }

        [UseProjection]
        public async Task<CourseType> Course([Service] CourseDataLoader courseDataLoader)
        {
            var course = await courseDataLoader.LoadAsync(CourseId);
            return new CourseType()
            {
                Id = course.Id,
                Name = course.Name,
                ProgramFileUrl = course.ProgramFileUrl,
                CoursePrice = course.CoursePrice,
                EquipmentPriceWithRobot = course.EquipmentPriceWithRobot,
                EquipmentPriceWithoutRobot = course.EquipmentPriceWithoutRobot,
                DurationInMonths = course.DurationInMonths
            };
        }

    }
}
