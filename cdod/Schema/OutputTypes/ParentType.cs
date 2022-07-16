using cdod.Models;
using cdod.Services;
using cdod.Services.DataLoaders;

namespace cdod.Schema.OutputTypes
{
    public class ParentType
    {
        private readonly User _user;

        public ParentType(User user)
        {
            _user = user;
        }

        [IsProjected]
        public int Id { get; set; }

        public string FirstName => _user.Firstname;
        public string LastName => _user.Lastname;
        public string? Patronymic => _user.Patronymic;
        public string? PhoneNumber => _user.PhoneNumber;
        public string Email => _user.Email;
        public string Password => _user.Password;
        public DateOnly? Birthday => _user.Birthday;
        public string? Address => _user.Address;
        public string? Education => _user.Education;
        public string? Inn => _user.Inn;
        public string? Snils => _user.Snils;
        public string? passportNo => _user.passportNo;
        public string? passportIssue => _user.passportIssue;
        public DateOnly? passportDate => _user.passportDate;
        public string? passportCode => _user.passportCode;
        public bool IsAdmin => _user.IsAdmin;

        public string? SecondPhoneNumber { get; set; }

        public string? SecondEmail { get; set; }

        [GraphQLName("applyingDate")]
        public DateOnly SignDate { get; set; }
    }
}
