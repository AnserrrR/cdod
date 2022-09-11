namespace cdod.Schema.InputTypes
{
    public class TeacherInput
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Patronymic { get; set; }

        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public DateOnly? Birthday { get; set; }
        public string? Address { get; set; }
        public string? Education { get; set; }
        public string? Inn { get; set; }
        public string? Snils { get; set; }
        public string? PassportNo { get; set; }
        public string? PassportIssue { get; set; }
        public DateOnly? PassportDate { get; set; }
        public string? PassportCode { get; set; }
        public bool? IsAdmin { get; set; }
        public string? WorkPlace { get; set; }
        public int? PostId { get; set; }
        public bool? IsInternal { get; set; }
        public int? WageRateId { get; set; }
    }
}
