namespace cdod.Schema.InputTypes
{
    public class StudentCreateInput
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Patronymic { get; set; }
        public string? Description { get; set; }
        public DateOnly? BirthDate { get; set; }
        public int? SchoolId { get; set; }
        public int ParentId { get; set; }
    }

    public class StudentUpdateInput
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Patronymic { get; set; }
        public string? Description { get; set; }
        public DateOnly? BirthDate { get; set; }
        public int? SchoolId { get; set; }
        public int? ParentId { get; set; }

    }
}
