namespace cdod.Schema.InputTypes
{
    public class TopicInputCreate
    {
        public string Title { get; set; }
        public int ProgrammId { get; set; }
    }

    public class TopicInputUpdate
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int? ProgrammId { get; set; }
    }
}
