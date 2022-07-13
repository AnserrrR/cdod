namespace cdod.Schema.InputTypes
{
    public class GroupCreateInput
    {
        public string Name { get; set; }
        public int TeacherId { get; set; }
        public int StartYear { get; set; }
        public int CourseId { get; set; }
    }

    public class GroupUpdateInput
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? TeacherId { get; set; }
        public int? StartYear { get; set; }
    }

}
