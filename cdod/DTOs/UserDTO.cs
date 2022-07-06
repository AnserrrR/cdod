using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cdodDTOs.DTOs
{
    [Index("PhoneNumber")]
    public class UserDTO
    {
        [Key]
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string? Patronymic { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateOnly Birthday { get; set; }
        public string? Address { get; set; }
        public string Education { get; set; }
        public string Inn { get; set; }
        public string Snils { get; set; }
        public string passportNo { get; set; }
        public string passportIssue { get; set; }
        public DateOnly passportDate { get; set; }
        public string passportCode { get; set; }
        public bool IsAdmin { get; set; }

        public IEnumerable<PayNoteDTO?> PayNotes { get; set; } = new List<PayNoteDTO?>();
        public IEnumerable<AnnouncementDTO?> Announcements { get; set; } = new List<AnnouncementDTO?>();


    }
}
