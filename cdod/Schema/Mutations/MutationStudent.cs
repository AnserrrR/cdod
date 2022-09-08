using cdod.Schema.InputTypes;
using cdod.Services;
using cdod.Models;
using cdod.Schema.OutputTypes;
using HotChocolate.AspNetCore.Authorization;

namespace cdod.Schema.Mutations
{
    [ExtendObjectType(typeof(Mutation))]
    public class MutationStudent
    {
        [Authorize(Roles = new[] { "admin" })]
        [UseDbContext(typeof(CdodDbContext))]
        public async Task<StudentType> StudentCreate(StudentCreateInput student, [ScopedService] CdodDbContext dbContext)
        {
            Student studentCreated = new Student()
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                Patronymic = student.Patronymic,
                BirthDate = student.BirthDate,
                Description = student.Description,
                SchoolId = student.SchoolId,
                ParentId = student.ParentId
            };
            dbContext.Students.Add(studentCreated);
            await dbContext.SaveChangesAsync();
            StudentType studentOutput = new StudentType()
            {
                Id = studentCreated.Id,
                LastName = studentCreated.LastName,
                FirstName = studentCreated.FirstName,
                Patronymic = studentCreated.Patronymic,
                BirthDate = studentCreated.BirthDate,
                Description = studentCreated.Description,
                SchoolId = studentCreated.SchoolId,
                ParentId = studentCreated.ParentId
            };
            return studentOutput;
        }

        [Authorize(Roles = new[] { "admin" })]
        [UseDbContext(typeof(CdodDbContext))]
        public async Task<bool> StudentUpdateMany(List<StudentUpdateInput> students, [ScopedService] CdodDbContext dbContext)
        {
            List<int> errorStudentIds = new List<int>();
            List<Student> studentUpdated = new List<Student>();
            foreach (StudentUpdateInput el in students)
            {
                Student? student = dbContext.Students.FirstOrDefault(s => s.Id == el.Id);
                if (student == null) { errorStudentIds.Add(el.Id); continue; }
                student.FirstName = el.FirstName ?? student.FirstName;
                student.LastName = el.LastName ?? student.LastName;
                student.Patronymic = el.Patronymic ?? student.Patronymic;
                student.BirthDate = el.BirthDate ?? student.BirthDate;
                student.Description = el.Description ?? student.Description;
                student.SchoolId = el.SchoolId ?? student.SchoolId;
                student.ParentId = el.ParentId ?? student.ParentId;

                Student? studentEdited = dbContext.Students.Find(el.Id);
                if (studentEdited == null) { errorStudentIds.Add(el.Id); continue; }
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

        [Authorize(Roles = new[] { "admin" })]
        [UseDbContext(typeof(CdodDbContext))]
        public async Task<bool> StudentDeleteMany(List<int> studentIds, [ScopedService] CdodDbContext dbContext)
        {
            List<int> errorNotStudentsIds = new List<int>();
            foreach (int id in studentIds)
            {
                Student? student = dbContext.Students.FirstOrDefault(s => s.Id == id);
                if (student is null) { errorNotStudentsIds.Add(id); continue; }
                dbContext.Students.Remove(student);
            }
            if (errorNotStudentsIds.Count() > 0) throw new GraphQLException($"Невозможно удалить следующих пользователей:\n" +
    $"ID следующих пользователей не являющихся родителями: {string.Join(" ", errorNotStudentsIds)}");
            return await dbContext.SaveChangesAsync() > 0;
        }

    // Работа с файлами Не закончена, не понимаю что хотят 
        [Authorize(Roles = new[] { "admin" })]
        [UseDbContext(typeof(CdodDbContext))]
        public async Task<bool> StudentStudyCreateMany(List<StudentToCourseCreateInput> StudentToCreate, [ScopedService] CdodDbContext dbContext, CancellationToken cancellationToken)
        {
            foreach(var StudentToCourseRecord in StudentToCreate)
            {
                IFile? contract = StudentToCourseRecord.Contract;
                string ContractDirectory = "../StaticFiles/Contracts";
                
                using var stream = File.Create(System.IO.Path.Combine(ContractDirectory, $"S{StudentToCourseRecord.StudentId}C{StudentToCourseRecord.CourseId}A{StudentToCourseRecord.Contract.Name}.pdf"));
                await contract.CopyToAsync(stream, cancellationToken);
                
                

            }
            return true;
        }
    }
}
