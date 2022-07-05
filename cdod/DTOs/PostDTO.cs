using System.ComponentModel.DataAnnotations;

namespace cdodDTOs.DTOs
{
    public class PostDTO
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
