using cdods.s;
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

        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            using(CdodDbContext context = _dbContext.CreateDbContext())
            {
                return await context.Students.ToListAsync();
            }
        }

        public async Task<Student> GetStudentById(int studentId)
        {
            using (CdodDbContext context = _dbContext.CreateDbContext())
            {
                return await context.Students.FirstOrDefaultAsync(u => u.Id == studentId);
            }
        }

        public async Task<IEnumerable<Student>> GetManyStudentsById(IReadOnlyList<int> studentIds)
        {
            using (CdodDbContext context = _dbContext.CreateDbContext())
            {
                return await context.Students.Where(i => studentIds.Contains(i.Id)).ToListAsync();
            }
        }

        public async Task<Student> CreateStudent(Student student)
        {
            using (CdodDbContext context = _dbContext.CreateDbContext())
            {
                context.Students.Add(student);
                await context.SaveChangesAsync();
                return student;
            }
        }

        public async Task<Student> UpdateStudent(Student student)
        {
            using (CdodDbContext context = _dbContext.CreateDbContext())
            {
                context.Students.Update(student);
                await context.SaveChangesAsync();
                return student;
            }
        }

        public async Task<bool> DeleteStudent(int studentId)
        {
            using (CdodDbContext context = _dbContext.CreateDbContext())
            {
                Student student = new Student() { Id = studentId };
                context.Students.Remove(student);
                return await context.SaveChangesAsync() > 0;
            }
        }
    }
}
