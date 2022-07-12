namespace cdod.Schema.InputTypes
{
    public class TeacherCreateInput
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string? Patronymic { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateOnly Birthday { get; set; }
        public string? Address { get; set; }
        public string? Education { get; set; }
        public string? Inn { get; set; }
        public string? Snils { get; set; }
        public string? passportNo { get; set; }
        public string? passportIssue { get; set; }
        public DateOnly? passportDate { get; set; }
        public string? passportCode { get; set; }
        public bool? isAdmin { get; set; }
        public string WorkPlace { get; set; }
        public int PostId { get; set; }
    }

    public class TeacherUpdateInput
    {
        public int Id { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Patronymic { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public DateOnly? Birthday { get; set; }
        public string? Address { get; set; }
        public string? Education { get; set; }
        public string? Inn { get; set; }
        public string? Snils { get; set; }
        public string? passportNo { get; set; }
        public string? passportIssue { get; set; }
        public DateOnly? passportDate { get; set; }
        public string? passportCode { get; set; }
        public string? WorkPlace { get; set; }
        public int? PostId { get; set; }
    }
}
