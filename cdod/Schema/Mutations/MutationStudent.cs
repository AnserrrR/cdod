using cdod.Schema.InputTypes;
using cdod.Services;
using cdods.s;

namespace cdod.Schema.Mutations
{
    [ExtendObjectType(typeof(Mutation))]
    public class MutationStudent
    {
        [UseDbContext(typeof(CdodDbContext))]
        public async Task<Student> CreateStudent(StudentInput studentForm, [ScopedService] CdodDbContext dbContext)
        {
            Student student = new Student()
            {
                FirstName = studentForm.FirstName,
                LastName = studentForm.LastName,
                Patronymic = studentForm.Patronymic,
                BirthDate = (DateOnly)studentForm.BirthDate,
                Descriotion = studentForm.Descriotion,
                SchoolId = (int)studentForm.SchoolId,
                ParentId = (int)studentForm.ParentId
            };
            dbContext.Students.Add(student);
            await dbContext.SaveChangesAsync();
            return student;
        }

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<Student> UpdateStudent(int studentId, StudentInput studentForm, [ScopedService] CdodDbContext dbContext)
        {
            Student? student = dbContext.Students.FirstOrDefault(s => s.Id == studentId);
            if (student == null) throw new Exception("Указанного студентва не существует");
            student.FirstName = studentForm.FirstName ?? student.FirstName;
            student.LastName = studentForm.LastName ?? student.LastName;
            student.Patronymic = studentForm.Patronymic ?? student.Patronymic;
            student.BirthDate = studentForm.BirthDate ?? student.BirthDate;
            student.Descriotion = studentForm.Descriotion ?? student.Descriotion;
            student.SchoolId = studentForm.SchoolId ?? student.SchoolId;

            dbContext.Students.Update(student);
            await dbContext.SaveChangesAsync();
            return student;
        }

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<bool> DeleteStudent(int studentId, [ScopedService] CdodDbContext dbContext)
        {
            Student? student = dbContext.Students.FirstOrDefault(s => s.Id == studentId);
            if (student == null) throw new Exception("Указанного студентва не существует");
            dbContext.Students.Remove(student);   
            return await dbContext.SaveChangesAsync() > 0;
        }

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<bool> DeleteManyStudents(List<int> studentIds, [ScopedService] CdodDbContext dbContext)
        {
            dbContext.Students.RemoveRange(studentIds.Select(s =>
            {
                Student? bufStudent = dbContext.Students.FirstOrDefault(sid => sid.Id == s);
                if (bufStudent == null) throw new Exception($"Вы указали для удаления несущесствующего ученика ID:{s}");
                return bufStudent;
            }));
            return await dbContext.SaveChangesAsync() > 0;
        }
    }
}
