using cdodDTOs.DTOs;
using Microsoft.EntityFrameworkCore;

namespace cdod.Services.Repositories
{
    public class StudentsRepository
    {
        private readonly IDbContextFactory<CdodDbContext> _dbContext;
        StudentsRepository(IDbContextFactory<CdodDbContext> dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<StudentDTO>> GetAllStudents()
        {
            using(CdodDbContext context = _dbContext.CreateDbContext())
            {
                return await context.Students.ToListAsync();
            }
        }

        public async Task<StudentDTO> GetStudentById(int studentId)
        {
            using (CdodDbContext context = _dbContext.CreateDbContext())
            {
                return await context.Students.FirstOrDefaultAsync(u => u.Id == studentId);
            }
        }

        public async Task<IEnumerable<StudentDTO>> GetManyStudentsById(IReadOnlyList<int> studentIds)
        {
            using (CdodDbContext context = _dbContext.CreateDbContext())
            {
                return await context.Students.Where(i => studentIds.Contains(i.Id)).ToListAsync();
            }
        }

        public async Task<StudentDTO> CreateStudent(StudentDTO studentDTO)
        {
            using (CdodDbContext context = _dbContext.CreateDbContext())
            {
                context.Students.Add(studentDTO);
                await context.SaveChangesAsync();
                return studentDTO;
            }
        }

        public async Task<StudentDTO> UpdateStudent(StudentDTO studentDTO)
        {
            using (CdodDbContext context = _dbContext.CreateDbContext())
            {
                context.Students.Update(studentDTO);
                await context.SaveChangesAsync();
                return studentDTO;
            }
        }

        public async Task<bool> DeleteStudent(int studentId)
        {
            using (CdodDbContext context = _dbContext.CreateDbContext())
            {
                StudentDTO student = new StudentDTO() { Id = studentId };
                context.Students.Remove(student);
                return await context.SaveChangesAsync() > 0;
            }
        }
    }
}
