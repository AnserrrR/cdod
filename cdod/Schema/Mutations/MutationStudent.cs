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
        public async Task<bool> StudentStudyCreateMany(List<StudentToCourseCreateInput> studentToCourseCreate, [ScopedService] CdodDbContext dbContext)
        {
            List<StudentToCourse> stcMany = new List<StudentToCourse>();
            List<string> errorsToCreate = new List<string>();
            foreach (var studentToCourseRecord in studentToCourseCreate)
            {
                StudentToCourse stc = new StudentToCourse()
                {
                    CourseId = studentToCourseRecord.CourseId,
                    StudentId = studentToCourseRecord.StudentId,
                    GroupId = studentToCourseRecord.GroupId,
                    IsGetRobot = studentToCourseRecord.IsGetRobot,
                    SignDate = studentToCourseRecord.AdmissionDate ?? DateOnly.FromDateTime(DateTime.UtcNow.Date),
                    ContractState = studentToCourseRecord.ContractState
                };
                stcMany.Add(stc);
            }
            dbContext.StudentToCourses.AddRange(stcMany);
            
            return await dbContext.SaveChangesAsync() > 0;
        }

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<bool> StudentStudyUpdateMany(List<StudentToCourseUpdateInput> studentToCourseUpdate, [ScopedService] CdodDbContext dbContext)
        {
            List<StudentToCourse> stcMany = new List<StudentToCourse>();
            List<string> errorsToUpdate = new List<string>();
            foreach (var studentToCourseRecord in studentToCourseUpdate)
            {
                StudentToCourse? studentToCourse = dbContext.StudentToCourses.FirstOrDefault(stc => ((stc.CourseId == studentToCourseRecord.CourseId )
                                                                                                 && (stc.StudentId == studentToCourseRecord.StudentId)
                                                                                                 && (stc.Attempt == studentToCourseRecord.Attempt)));
                if (studentToCourse == null) { errorsToUpdate.Add($"{studentToCourseRecord.CourseId}, {studentToCourseRecord.StudentId}, {studentToCourseRecord.Attempt};"); continue; }

                studentToCourse.GroupId = studentToCourseRecord.GroupId ?? studentToCourse.GroupId;
                studentToCourse.ContractState = studentToCourseRecord.ContractState ?? studentToCourse.ContractState;
                studentToCourse.SignDate = studentToCourseRecord.AdmissionDate ?? studentToCourse.SignDate;
                studentToCourse.ContractUrl = studentToCourseRecord.ContractUrl ?? studentToCourse.ContractUrl;
                studentToCourse.IsGetRobot = studentToCourseRecord.IsGetRobot ?? studentToCourse.IsGetRobot;
                stcMany.Add(studentToCourse);

            }
            dbContext.StudentToCourses.UpdateRange(stcMany);
            if(errorsToUpdate.Count() > 0) { throw new GraphQLException($"Невозможно обновить следующие записи привязки студента к курсу или группе: " +
                $"{string.Join("\n", errorsToUpdate)}"); }

            return await dbContext.SaveChangesAsync() > 0;
        }

        public async Task<Boolean> StudentsStudyDeleteMany(List<StudentToCourseDetachInput> studentToCourseDetach, [ScopedService] CdodDbContext dbContext)
        {
            List<StudentToCourseDetachInput> errorsIds = new List<StudentToCourseDetachInput>();
            List<StudentToCourse> stcToDelete = new List<StudentToCourse>();
            foreach (var el in studentToCourseDetach)
            {
                StudentToCourse? studentToCourse = dbContext.StudentToCourses.FirstOrDefault(stc => ((stc.CourseId == el.CourseId)
                                                                                                && (stc.StudentId == el.StudentId)
                                                                                                && (stc.Attempt == el.Attempt)));
                if (studentToCourse is null) { errorsIds.Add(el); continue; }
                stcToDelete.Add(studentToCourse);
            }
            dbContext.StudentToCourses.RemoveRange(stcToDelete);
            if (errorsIds.Count() > 0)
            {
                throw new GraphQLException($"Невозможно обновить следующие записи привязки студента к курсу или группе: " +
                $"{string.Join("\n", errorsIds)}");
            }
            return await dbContext.SaveChangesAsync() > 0;
        }
    }
}
