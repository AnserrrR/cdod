using System.ComponentModel.DataAnnotations;

namespace cdodDTOs.DTOs
{
    public class ContractStateDTO
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<StudentToCourseDTO?> StudentsToCourses { get; set; } = new List<StudentToCourseDTO?>();
    }
}
