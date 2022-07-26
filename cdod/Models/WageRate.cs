using System.ComponentModel.DataAnnotations;

namespace cdod.Models
{
    public class WageRate
    {
        [Key]
        public int Id { get; set; }

        public double Rate { get; set; }
    }
}
