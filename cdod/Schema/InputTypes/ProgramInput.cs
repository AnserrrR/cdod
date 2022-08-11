namespace cdod.Schema.InputTypes
{
    public class ProgramInputCreate
    {
        public string? name { get; set; }
        public int hours { get; set; }

        public string? programFileUrl { get; set; }
    }

    public class ProgramInputUpdate
    {
        public string? name { set; get; }
        public int? hours { get; set; }
        public string? programFileUrl { get; set; }
    }
}
