using cdod.Services.DataLoaders;

namespace cdod.Schema.OutputTypes
{
    public class LessonType
    {
        [IsProjected]
        public int Id { get; set; }

        public string Homework { get; set; }

        public DateTime StartDateTime { get; set; }

        public int Duration { get; set; }

        public string ClassRoom { get; set; }

        [GraphQLName("topic")]
        public string LessonTopic { get; set; }

        [IsProjected]
        public int GroupId { get; set; }

        [UseProjection]
        public async Task<GroupType> Group([Service] GroupDataLoader groupDataLoader)
        {
            var group = await groupDataLoader.LoadAsync(GroupId);

            return new GroupType()
            {
                Id = group.Id,
                Name = group.Name,
                StartDate = group.StartDate,
                CourseId = group.CourseId,
                TeacherId = group.TeacherId
            };
        }
    }
}
