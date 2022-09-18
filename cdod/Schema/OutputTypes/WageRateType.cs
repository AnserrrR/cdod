namespace cdod.Schema.OutputTypes
{
    public class WageRateType
    {
        [IsProjected]
        public int Id { get; set; }
        public double Rate { get; set; }
    }
}
