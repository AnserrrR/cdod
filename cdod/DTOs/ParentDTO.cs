using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cdodDTOs.DTOs
{
    public class ParentDTO
    {
        [Key]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public UserDTO User { get; set; }
        public string? SecondPhoneNumber { get; set; }
        public string? SecondEmail { get; set; }
        public DateOnly SignDate { get; set; }

        public IEnumerable<StudentDTO?> Students { get; set; } = new List<StudentDTO?>();

    }
}
