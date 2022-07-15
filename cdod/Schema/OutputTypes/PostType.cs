namespace cdod.Schema.OutputTypes
{
    public class PostType
    {
        [IsProjected]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
