namespace cdod.Schema.InputTypes
{
    public class LessonCreateInput
    {
        public string? Homework { get; set; }

        public DateTime StartTime { get; set; }

        public int Duration { get; set; }

        public string? ClassRoom { get; set; }

        public string? LessonTopic { get; set; }

        public int GroupId { get; set; }
    }

    public class LessonUpdateInput
    {
        public int Id { get; set; }

        public string? Homework { get; set; }

        public DateTime? StartTime { get; set; }

        public int? Duration { get; set; }

        public string? ClassRoom { get; set; }

        public string? LessonTopic { get; set; }

        public int? GroupId { get; set; }
    }

}
