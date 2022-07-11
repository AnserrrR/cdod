using cdod.Models;
using cdod.Services;

namespace cdod.Schema.OutputTypes
{
    public class ParentType
    {
        [IsProjected]
        public int UserId { get; set; }

        [UseDbContext(typeof(CdodDbContext))]
        [UseProjection]
        public async Task<User> User([ScopedService]CdodDbContext context)
        {
            User user = await context.Users.FindAsync(UserId);
            return user;
        }

        public string? SecondPhoneNumber { get; set; }

        public string? SecondEmail { get; set; }

        public DateOnly SignDate { get; set; }

        public IEnumerable<Student?> Students { get; set; } = new List<Student?>();
    }
}
