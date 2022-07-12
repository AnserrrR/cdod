using cdods.s;
namespace cdod.Schema.InputTypes
{
    public class SchoolCreateInput
    {
        public string Name { get; set; }
        public District District { get; set; }
    }

    public class SchoolUpdateInput
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public District? District { get; set; }
    }

}
