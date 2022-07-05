using System.ComponentModel.DataAnnotations;

namespace cdodDTOs.DTOs
{
    public enum Appointment
    {
        Course,
        Material
    }
    public class PayNoteDTO
    {
        [Key]
        public int Id { get; set; }
        public decimal Sum { get; set; }
        public DateOnly Date { get; set; }
        public Appointment Appointment { get; set; }
        public string CheckURL { get; set; }

        public int StudentId { get; set; }
        public StudentDTO Student { get; set; }

        public int UserId { get; set; }
        public UserDTO User { get; set; }
    }
}
