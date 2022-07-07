using cdods.s;
using Microsoft.EntityFrameworkCore;

namespace cdod.Services.Repositories
{
    public class SchoolsRepository
    {
        private readonly IDbContextFactory<CdodDbContext> _dbContext;
        SchoolsRepository(IDbContextFactory<CdodDbContext> dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<School>> GetAllStudents()
        {
            using (CdodDbContext context = _dbContext.CreateDbContext())
            {
                return await context.Schools.ToListAsync();
            }
        }

        public async Task<School> GetStudentById(int studentId)
        {
            using (CdodDbContext context = _dbContext.CreateDbContext())
            {
                return await context.Schools.FirstOrDefaultAsync(u => u.Id == studentId);
            }
        }

        public async Task<School> CreateStudent(School student)
        {
            using (CdodDbContext context = _dbContext.CreateDbContext())
            {
                context.Schools.Add(student);
                await context.SaveChangesAsync();
                return student;
            }
        }

        public async Task<School> UpdateStudent(School student)
        {
            using (CdodDbContext context = _dbContext.CreateDbContext())
            {
                context.Schools.Update(student);
                await context.SaveChangesAsync();
                return student;
            }
        }

        public async Task<bool> DeleteStudent(int studentId)
        {
            using (CdodDbContext context = _dbContext.CreateDbContext())
            {
                School school = new School() { Id = studentId };
                context.Schools.Remove(school);
                return await context.SaveChangesAsync() > 0;
            }
        }
    }
}
