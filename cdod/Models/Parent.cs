using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using cdod.Schema.OutputTypes;

namespace cdod.Models
{
    public enum RelationType
    {
        Mother,
        Father,
        Grandma,
        Grandpa,
        Aunt,
        Uncle,
        Brother,
        Sister,
        Guardian,
        Other
    }

    public class Parent
    {
        [Key]
        [ForeignKey("User")]
        public int UserId { get; set; }

        public User User { get; set; }

        public string? SecondPhoneNumber { get; set; }

        public string? SecondEmail { get; set; }

        public DateOnly SignDate { get; set; }

        public RelationType? Type { get; set; }

        public IEnumerable<Student?> Students { get; set; } = new List<Student?>();

    }
}
