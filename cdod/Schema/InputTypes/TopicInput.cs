namespace cdod.Schema.InputTypes
{
    public class TopicInputCreate
    {
        public string title { get; set; }
        public int programmId { get; set; }
    }

    public class TopicInputUpdate
    {
        public int id { get; set; }
        public string? title { get; set; }
        public int? programmId { get; set; }
    }
}
