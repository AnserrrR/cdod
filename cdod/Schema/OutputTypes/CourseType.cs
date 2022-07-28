namespace cdod.Schema.OutputTypes
{
    public class CourseType
    {
        [IsProjected]
        public int Id { get; set; }

        public string Name { get; set; }

        public int? ProgramId { get; set; }

        [GraphQLName("price")]
        public double CoursePrice { get; set; }

        public string? Color { get; set; }

        public string? SvgIconUrl { get; set; }

        public double? EquipmentPriceWithRobot { get; set; }

        public double? EquipmentPriceWithoutRobot { get; set; }

        public int DurationInMonths { get; set; }
    }
}
