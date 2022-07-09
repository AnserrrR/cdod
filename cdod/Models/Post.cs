using System.ComponentModel.DataAnnotations;

namespace cdod.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
