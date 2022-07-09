using System.ComponentModel.DataAnnotations;

namespace cdod.Models
{
    public class ContractState
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<StudentToCourse?> StudentsToCourses { get; set; } = new List<StudentToCourse?>();
    }
}
