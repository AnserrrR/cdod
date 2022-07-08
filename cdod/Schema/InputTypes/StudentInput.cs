namespace cdod.Schema.InputTypes
{
    public class StudentInput
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Patronymic { get; set; }
        public string? Descriotion { get; set; }
        public DateOnly? BirthDate { get; set; }
        public int? SchoolId { get; set; }
        public int? ParentId { get; set; }
    }
}
