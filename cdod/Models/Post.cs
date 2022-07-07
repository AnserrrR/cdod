using System.ComponentModel.DataAnnotations;

namespace cdods.s
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
