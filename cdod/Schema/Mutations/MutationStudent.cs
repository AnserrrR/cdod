using cdod.Schema.InputTypes;
using cdod.Services;
using cdodDTOs.DTOs;

namespace cdod.Schema.Mutations
{
    [ExtendObjectType(typeof(Mutation))]
    public class MutationStudent
    {
        [UseDbContext(typeof(CdodDbContext))]
        public async Task<StudentDTO> CreateStudent(StudentInput studentForm, [ScopedService] CdodDbContext dbContext)
        {
            StudentDTO student = new StudentDTO()
            {
                FirstName = studentForm.FirstName,
                LastName = studentForm.LastName,
                Patronymic = studentForm.Patronymic,
                BirthDate = studentForm.BirthDate,
                Descriotion = studentForm.Descriotion,
                SchoolId = studentForm.SchoolId,
                ParentId = studentForm.ParentId
            };
            dbContext.Students.Add(student);
            await dbContext.SaveChangesAsync();
            return student;
        }

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<StudentDTO> UpdateStudent(int studentId, StudentInput studentForm, [ScopedService] CdodDbContext dbContext)
        {
            StudentDTO student = new StudentDTO()
            {
                Id = studentId,
                FirstName = studentForm.FirstName,
                LastName = studentForm.LastName,
                Patronymic = studentForm.Patronymic,
                BirthDate = studentForm.BirthDate,
                Descriotion = studentForm.Descriotion,
                SchoolId = studentForm.SchoolId,
                ParentId = studentForm.ParentId
            };
            dbContext.Students.Update(student);
            await dbContext.SaveChangesAsync();
            return student;
        }

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<bool> DeleteStudent(int studentId, [ScopedService] CdodDbContext dbContext)
        {
            StudentDTO student = new StudentDTO()
            {
                Id = studentId
            };
            dbContext.Students.Remove(student);   
            return await dbContext.SaveChangesAsync() > 0;
        }

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<bool> DeleteManyStudents(List<int> studentIds, [ScopedService] CdodDbContext dbContext)
        {
            List<StudentDTO> students=new List<StudentDTO>();
            foreach (int studentId in studentIds) students.Add(new StudentDTO() { Id = studentId });
            //dbContext.Students.RemoveRange(studentIds.Select(s => new StudentDTO() { Id = s }));
            return await dbContext.SaveChangesAsync() > 0;
        }
    }
}
