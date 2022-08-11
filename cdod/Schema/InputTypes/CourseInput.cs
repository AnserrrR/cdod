namespace cdod.Schema.InputTypes
{

    public class CourseCreateInput
    {
        public string name { get; set; }
        public int? programId { get; set; }
        public double? coursePrice { get; set; }
        public double? equipmentPriceWithRobot { get; set; }
        public double? equipmentPriceWithoutRobot { get; set; }
        public string? color { get; set; }
        public string? svgIconColor { get; set; }
    }

    public class CourseUpdateInput
    {
        public int Id { get; set; }
        public string? name { get; set; }
        public int? programId { get; set; }
        public double? coursePrice { get; set; }
        public double? equipmentPriceWithRobot { get; set; }
        public double? equipmentPriceWithoutRobot { get; set; }
        public string? color { get; set; }
        public string? svgIconColor { get; set; }
    }
}
