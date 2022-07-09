using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace cdod.Models
{
    public enum Appointment
    {
        Course,
        Material
    }
    public class PayNote
    {
        [Key]
        public int Id { get; set; }
        public double Sum { get; set; }
        public DateOnly Date { get; set; }
        public Appointment Appointment { get; set; }
        public string CheckURL { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
