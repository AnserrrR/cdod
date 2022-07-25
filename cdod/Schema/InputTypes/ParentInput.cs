namespace cdod.Schema.InputTypes
{
    public class ParentLoginInput
    {
        public string email { get; set; }
        public string password { get; set; }
    }

    public class ParentCreateInput
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
        public string? SecondPhoneNumber { get; set; }
        public string? SecondEmail { get; set; }
        public DateOnly applyingDate { get; set; }
    }
    
    public class ParentUpdateInput
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
        public string? SecondPhoneNumber { get; set; }
        public string? SecondEmail { get; set; }
        public DateOnly? applyingDate { get; set; }
    }
}
