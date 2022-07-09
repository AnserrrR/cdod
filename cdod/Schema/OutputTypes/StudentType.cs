using cdod.Models;
using cdod.Services;
using Microsoft.EntityFrameworkCore;
using Group = System.Text.RegularExpressions.Group;

namespace cdod.Schema.OutputTypes
{
    public class StudentType
    {
        [IsProjected]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string? Patronymic { get; set; }

        public string? Descriotion { get; set; }

        public DateOnly BirthDate { get; set; }

        public int SchoolId { get; set; }

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<School> School([ScopedService] CdodDbContext context)
        {
            School school = await context.Schools.FindAsync(SchoolId);
            return school;
        }

        public int ParentId { get; set; }

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<Parent> Parent([ScopedService] CdodDbContext context)
        {
            Parent parent = await context.Parents.FindAsync(ParentId);
            return parent;
        }

        [UseDbContext(typeof(CdodDbContext))]
        public IQueryable<InfoType> Info([ScopedService]CdodDbContext context)
        {
            return  context.StudentToCourses
                .Where(stc => stc.StudentId == Id)
                .Select(stc => new InfoType()
                {
                    CourseId = stc.CourseId,
                    StudentId = Id
                });
        }
    }
}
