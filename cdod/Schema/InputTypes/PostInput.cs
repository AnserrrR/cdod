namespace cdod.Schema.InputTypes
{
    public class PostCreateInput
    {
        public string Name { get; set; }
    }

    public class PostUpdateInput
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
