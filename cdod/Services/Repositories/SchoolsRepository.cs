using cdodDTOs.DTOs;
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

        public async Task<IEnumerable<SchoolDTO>> GetAllStudents()
        {
            using (CdodDbContext context = _dbContext.CreateDbContext())
            {
                return await context.Schools.ToListAsync();
            }
        }

        public async Task<SchoolDTO> GetStudentById(int studentId)
        {
            using (CdodDbContext context = _dbContext.CreateDbContext())
            {
                return await context.Schools.FirstOrDefaultAsync(u => u.Id == studentId);
            }
        }

        public async Task<SchoolDTO> CreateStudent(SchoolDTO studentDTO)
        {
            using (CdodDbContext context = _dbContext.CreateDbContext())
            {
                context.Schools.Add(studentDTO);
                await context.SaveChangesAsync();
                return studentDTO;
            }
        }

        public async Task<SchoolDTO> UpdateStudent(SchoolDTO studentDTO)
        {
            using (CdodDbContext context = _dbContext.CreateDbContext())
            {
                context.Schools.Update(studentDTO);
                await context.SaveChangesAsync();
                return studentDTO;
            }
        }

        public async Task<bool> DeleteStudent(int studentId)
        {
            using (CdodDbContext context = _dbContext.CreateDbContext())
            {
                SchoolDTO school = new SchoolDTO() { Id = studentId };
                context.Schools.Remove(school);
                return await context.SaveChangesAsync() > 0;
            }
        }
    }
}
