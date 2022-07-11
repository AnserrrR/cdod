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

        [IsProjected]
        public int SchoolId { get; set; }

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<School> School([ScopedService] CdodDbContext context)
        {
            return await context.Schools.FindAsync(SchoolId);  
        }

        [IsProjected]
        public int ParentId { get; set; }

        [UseDbContext(typeof(CdodDbContext))]
        [UseProjection]
        public async Task<ParentType> Parent([ScopedService] CdodDbContext context)
        {
            Parent parent = await context.Parents.FindAsync(ParentId);
            return new ParentType()
            {
                UserId = parent.UserId,
                SecondPhoneNumber = parent.SecondPhoneNumber,
                SecondEmail = parent.SecondEmail,
                SignDate = parent.SignDate
            };
        }

        [UseDbContext(typeof(CdodDbContext))]
        [UseProjection]
        public IQueryable<InfoType> Info([ScopedService]CdodDbContext context)
        {
            return  context.StudentToCourses
                .Where(stc => stc.StudentId == Id)
                .Select(stc => new InfoType()
                {
                    CourseId = stc.CourseId,
                    StudentId = Id,
                    AdmissionDate = stc.SignDate,
                    ContractStateId = stc.ContractStateId
                });
        }
    }
}
