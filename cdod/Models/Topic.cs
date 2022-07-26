using System.ComponentModel.DataAnnotations;

namespace cdod.Models
{
    public class Topic
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int ProgramId { get; set; }
        public CourseProgram Program { get; set; }
    }
}
