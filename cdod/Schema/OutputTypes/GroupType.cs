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

        [UseProjection]
        public async Task<TeacherType> Teacher([Service] TeacherDataLoader teacherDataLoader)
        {
            var teacher = await teacherDataLoader.LoadAsync(TeacherId);
            return new TeacherType()
            {
                UserId = teacher.UserId,
                PostId = teacher.PostId,
                WorkPlace = teacher.WorkPlace
            };
        }

        public DateOnly StartDate { get; set; }

        [IsProjected]
        public int CourseId { get; set; }

        [UseProjection]
        public async Task<Course> Course([Service] CourseDataLoader courseDataLoader)
        {
            var course = await courseDataLoader.LoadAsync(CourseId);
            return course;
        }

    }
}
