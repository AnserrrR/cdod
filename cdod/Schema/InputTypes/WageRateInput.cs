namespace cdod.Schema.InputTypes
{
    public class WageRateCreateInput
    {
        public double Rate { get; set; } 
    }

    public class WageRateUpdateInput
    {
        public int Id { get; set; }
        public double Rate { get; set; } 
    }
}
