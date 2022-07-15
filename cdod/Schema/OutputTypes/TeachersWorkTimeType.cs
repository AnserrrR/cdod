using cdod.Services.DataLoaders;

namespace cdod.Schema.OutputTypes
{
    public class TeachersWorkTimeType
    {
        [IsProjected]
        public int TeacherId { get; set; }

        public async Task<TeacherType> Teacher([Service] TeacherDataLoader teacherDataLoader,
            [Service] UserDataLoader userDataLoader)
        {
            var teacher = await teacherDataLoader.LoadAsync(TeacherId);

            return new TeacherType(await userDataLoader.LoadAsync(TeacherId))
            {
                Id = teacher.UserId,
                PostId = teacher.PostId,
                WorkPlace = teacher.WorkPlace
            };
        }

        [IsProjected]
        public int LessonId { get; set; }

        [UseProjection]
        public async Task<LessonType> Lesson([Service]LessonDataLoader lessonDataLoader)
        {
            var l = await lessonDataLoader.LoadAsync(LessonId);

            return new LessonType()
            {
                Id = l.Id,
                Homework = l.Homework,
                StartDateTime = l.StartDateTime,
                Duration = l.Duration,
                ClassRoom = l.ClassRoom,
                LessonTopic = l.LessonTopic,
                GroupId = l.GroupId
            };
        }

        public TimeOnly WorkTime { get; set; }
    }
}
