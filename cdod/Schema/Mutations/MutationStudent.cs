using cdod.Schema.InputTypes;
using cdod.Services;
using cdod.Models;
using cdod.Schema.OutputTypes;

namespace cdod.Schema.Mutations
{
    [ExtendObjectType(typeof(Mutation))]
    public class MutationStudent
    {
        [UseDbContext(typeof(CdodDbContext))]
        public async Task<Student> StudentCreate(StudentCreateInput student, [ScopedService] CdodDbContext dbContext)
        {
            Student _student = new Student()
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                Patronymic = student.Patronymic,
                BirthDate = student.BirthDate,
                Description = student.Descriotion,
                SchoolId = student.SchoolId,
                ParentId = student.ParentId
            };
            dbContext.Students.Add(_student);
            await dbContext.SaveChangesAsync();
            StudentType studentOutput = new StudentType()
            {
                Id = _student.Id,
                LastName = _student.LastName,
                FirstName = _student.FirstName,
                Patronymic = _student.Patronymic,
                BirthDate = _student.BirthDate,
                Description = _student.Description,
                SchoolId = _student.SchoolId,
                ParentId = _student.ParentId
            };
            return _student;
        }

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<bool> StudentUpdateMany(List<StudentUpdateInput> students, [ScopedService] CdodDbContext dbContext)
        {
            List<int> errorStudentIds = new List<int>();
            List<Student> studentUpdated = new List<Student>();
            foreach (StudentUpdateInput el in students)
            {
                Student? _student = dbContext.Students.FirstOrDefault(s => s.Id == el.Id);
                if (_student == null) { errorStudentIds.Add(el.Id); continue; }
                Student student = new Student()
                {
                    Id = el.Id,
                    FirstName = el.FirstName ?? _student.FirstName,
                    LastName = el.LastName ?? _student.LastName,
                    Patronymic = el.Patronymic ?? _student.Patronymic,
                    BirthDate = el.BirthDate ?? _student.BirthDate,
                    Description = el.Description ?? _student.Description,
                    SchoolId = el.SchoolId ?? _student.SchoolId,
                    ParentId = el.ParentId ?? _student.ParentId
                };
                studentUpdated.Add(student);
            }
            dbContext.Students.UpdateRange(studentUpdated);
            if (errorStudentIds.Count() > 0)
            {
                    throw new GraphQLException($"Невозможно обновить следующих пользователей:\n" +
                        $"ID следующих пользователей нет в системе: {string.Join(" ", errorStudentIds)}\n");
                
            }
            return await dbContext.SaveChangesAsync() > 0;
        }

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<bool> StudentDeleteMany(List<int> studentIds, [ScopedService] CdodDbContext dbContext)
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
