using cdod.Models;
using cdod.Services;

namespace cdod.Schema.OutputTypes
{
    public class SchoolType
    {
        [IsProjected]
        public int Id { get; set; }

        public string Name { get; set; }

        public District District { get; set; }

        [UseDbContext(typeof(CdodDbContext))]
        [UseProjection]
        public IQueryable<StudentType> Students([ScopedService] CdodDbContext context)
        {
            return context.Students
                .Where(s => s.SchoolId == Id)
                .Select(s => new StudentType()
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Patronymic = s.Patronymic,
                    Description = s.Description,
                    BirthDate = s.BirthDate,
                    SchoolId = s.SchoolId,
                    ParentId = s.ParentId
                });
        }
    }
}
