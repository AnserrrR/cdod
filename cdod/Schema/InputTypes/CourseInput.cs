namespace cdod.Schema.InputTypes
{

    public class CourseCreateInput
    {
        public string Name { get; set; }
        public int? ProgramId { get; set; }
        public double? CoursePrice { get; set; }
        public double? EquipmentPriceWithRobot { get; set; }
        public double? EquipmentPriceWithoutRobot { get; set; }
        public string? Color { get; set; }
        public string? SvgIconColor { get; set; }
        public int? DurationInMonths { get; set; }
    }

    public class CourseUpdateInput
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? ProgramId { get; set; }
        public double? CoursePrice { get; set; }
        public double? EquipmentPriceWithRobot { get; set; }
        public double? EquipmentPriceWithoutRobot { get; set; }
        public string? Color { get; set; }
        public string? SvgIconColor { get; set; }
        public int? DurationInMonths { get; set; }

    }
}
