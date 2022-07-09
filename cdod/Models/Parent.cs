using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cdod.Models
{
    public class Parent
    {
        [Key]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        public string? SecondPhoneNumber { get; set; }
        public string? SecondEmail { get; set; }
        public DateOnly SignDate { get; set; }

        public IEnumerable<Student?> Students { get; set; } = new List<Student?>();

    }
}
