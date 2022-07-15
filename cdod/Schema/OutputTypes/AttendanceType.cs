using cdod.Models;
using cdod.Services.DataLoaders;

namespace cdod.Schema.OutputTypes
{
    public class AttendanceType
    {
        [IsProjected]
        public int StudentId { get; set; }
        public async Task<StudentType> Student([Service]StudentDataLoader studentDataLoader)
        {
            var s = await studentDataLoader.LoadAsync(StudentId);

            return new StudentType()
            {
                Id = s.Id,
                SchoolId = s.SchoolId,
                ParentId = s.ParentId,
                BirthDate = s.BirthDate,
                Description = s.Description,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Patronymic = s.Patronymic
            };
        }

        [IsProjected]
        public int LessonId { get; set; }
        [UseProjection]
        public async Task<LessonType> Lesson([Service] LessonDataLoader lessonDataLoader)
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

        public int Mark { get; set; }

        public string Note { get; set; }

        public Status Status { get; set; }
    }
}
