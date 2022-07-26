namespace cdod.Models
{
    public class StudentsToGroups
    {
        public int StudentId { get; set; }

        public Student Student { get; set; }

        public int GroupId { get; set; }

        public Group Group { get; set; }
    }
}