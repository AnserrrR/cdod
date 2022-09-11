namespace cdod.Schema.InputTypes
{
    public class ProgramInputCreate
    {
        public string? Name { get; set; }
        public int Hours { get; set; }

        public string? ProgramFileUrl { get; set; }
    }

    public class ProgramInputUpdate
    {
        public string? Name { set; get; }
        public int? Hours { get; set; }
        public string? ProgramFileUrl { get; set; }
    }
}
