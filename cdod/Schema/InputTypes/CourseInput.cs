namespace cdod.Schema.InputTypes
{

    public class CourseCreateInput
    {
        public string name { get; set; }
        public string programFileUrl { get; set; }
        public double coursePrice { get; set; }
        public double? equipmentPriceWithRobot { get; set; }
        public double? equipmentPriceWithoutRobot { get; set; }
        public int durationInMonths { get; set; } // ????
    }

    public class CourseUpdateInput
    {
        int id { get; set; }
        public string? name { get; set; }
        public string? programFileUrl { get; set; }
        public double? coursePrice { get; set; }
        public double? equipmentPrice { get; set; }
        public int? durationInMonths { get; set; }
    }
}
