namespace cdod.Schema.InputTypes
{

    public class CourseInput
    {
        public string? Name { get; set; }
        public string? ProgramFileUrl { get; set; }
        public double? CoursePrice { get; set; }
        public double? EquipmentPrice { get; set; }
        public int? DurationInMonths { get; set; }
    }
}
