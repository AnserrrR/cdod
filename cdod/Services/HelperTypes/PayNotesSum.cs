using cdod.Models;

namespace cdod.Services.HelperTypes
{
    public class PayNotesSum
    {
        public int CourseId { get; set; }

        public int StudentId { get; set; }

        public int Attempt { get; set; }

        public Appointment Appointment { get; set; }

        public double TotalSum { get; set; }
    }
}
