using System.ComponentModel.DataAnnotations;

namespace cdodDTOs.DTOs
{
    public enum District
    {
        Central,
        Tractor
    }

    public class SchoolDTO
    {

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public District District { get; set; }

        public IEnumerable<StudentDTO> Students { get; set; }
    }
}
