using cdod.Models;
using cdod.Schema.InputTypes;
using cdod.Schema.OutputTypes;
using cdod.Services;
using HotChocolate.AspNetCore.Authorization;

namespace cdod.Schema.Mutations
{
    [ExtendObjectType(typeof(Mutation))]
    public class MutationTeacher
    {
        [Authorize(Roles = new[] { "admin" })]
        [UseDbContext(typeof(CdodDbContext))]
        public async Task<TeacherType> TeacherCreate(TeacherInput teacher, [ScopedService] CdodDbContext dbContext)
        {
            if (dbContext.Users.FirstOrDefault(u => u.Email == teacher.Email) is not null) throw new GraphQLException("Пользователь с таким емейлом уже существует");
            User user = new User()
            {
                Firstname = teacher.FirstName,
                Lastname = teacher.LastName,
                Patronymic = teacher.Patronymic,
                PhoneNumber = teacher.PhoneNumber,
                Email = teacher.Email,
                Password = teacher.Password,
                Birthday = teacher.Birthday,
                Address = teacher.Address,
                Education = teacher.Education,
                Inn = teacher.Inn,
                Snils = teacher.Snils,
                passportNo = teacher.PassportNo,
                passportIssue = teacher.PassportIssue,
                passportDate = teacher.PassportDate,
                passportCode = teacher.PassportCode,
            };
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();
            Teacher teacherSave = new Teacher()
            {
                UserId = user.Id,
                WorkPlace = teacher.WorkPlace,
                PostId = teacher.PostId,
                WageRateId = teacher.WageRateId ?? 0
            };

            dbContext.Teachers.Add(teacherSave);
            await dbContext.SaveChangesAsync();
            TeacherType patentOutput = new TeacherType(user)
            {
                Id = user.Id,
                WorkPlace = teacher.WorkPlace,
                PostId = teacher.PostId,
                // WageRateId = teacher.wageRateId ЖОРА РАСКОМЕНТЬ
            };

            return patentOutput;
        }

        [Authorize(Roles = new[] { "admin" })]
        [UseDbContext(typeof(CdodDbContext))]
        public async Task<bool> TeacherUpdate(int id, TeacherInput teacher, [ScopedService] CdodDbContext dbContext)
        {
            User? userUpdated = dbContext.Users.FirstOrDefault(u => u.Id == id);
            if (userUpdated == null)
            {
                throw new GraphQLException($"Невозможно обновить следующего преподователя:{id}");
            }
            Teacher? teacherUpdated = dbContext.Teachers.FirstOrDefault(t => t.UserId == id);
            if (teacherUpdated is null)
            {
                throw new GraphQLException($"Невозможно обновить следующего преподователя:{id}");
            }
            userUpdated.Firstname = teacher.FirstName ?? userUpdated.Firstname;
            userUpdated.Lastname = teacher.LastName ?? userUpdated.Lastname;
            userUpdated.Patronymic = teacher.Patronymic ?? userUpdated.Patronymic;
            userUpdated.PhoneNumber = teacher.PhoneNumber ?? userUpdated.PhoneNumber;
            userUpdated.Email = teacher.Email ?? userUpdated.Email;
            userUpdated.Password = teacher.Password ?? userUpdated.Password;
            userUpdated.Birthday = teacher.Birthday ?? userUpdated.Birthday;
            userUpdated.Address = teacher.Address ?? userUpdated.Address;
            userUpdated.Education = teacher.Education ?? userUpdated.Education;
            userUpdated.Inn = teacher.Inn ?? userUpdated.Inn;
            userUpdated.Snils = teacher.Snils ?? userUpdated.Snils;
            userUpdated.passportNo = teacher.PassportNo ?? userUpdated.passportNo;
            userUpdated.passportIssue = teacher.PassportIssue ?? userUpdated.passportIssue;
            userUpdated.passportDate = teacher.PassportDate ?? userUpdated.passportDate;
            userUpdated.passportCode = teacher.PassportCode ?? userUpdated.passportCode;
            teacherUpdated.WorkPlace = teacher.WorkPlace ?? teacherUpdated.WorkPlace;
            teacherUpdated.PostId = teacher.PostId ?? teacherUpdated.PostId;
            teacherUpdated.WageRateId = teacher.WageRateId ?? teacherUpdated.WageRateId;

            dbContext.Users.UpdateRange(userUpdated);
            dbContext.Teachers.UpdateRange(teacherUpdated);
            return await dbContext.SaveChangesAsync() > 0;
        }

        [Authorize(Roles = new[] { "admin" })]
        [UseDbContext(typeof(CdodDbContext))]
        public async Task<bool> TeacherDeleteMany(List<int> teacherIds, [ScopedService] CdodDbContext dbContext)
        {
            List<int> errorNotTeacherIds = new List<int>();
            try
            {
                dbContext.Teachers.RemoveRange(teacherIds.Select(t =>
                {
                    Teacher? _teacher = dbContext.Teachers.FirstOrDefault(tid => tid.UserId == t);
                    if (_teacher is null)
                    {
                        errorNotTeacherIds.Add(t);
                        throw new GraphQLException($"Невозможно удалить следующих пользователей:\n" +
                            $"ID следующих пользователей не являющихся родителями: {t}");
                    }
                    return _teacher;
                }));
            }
            catch
            {
                throw new GraphQLException($"Невозможно удалить следующих пользователей:\n" +
                    $"ID следующих пользователей не являющихся родителями: {string.Join(" ", errorNotTeacherIds)}");

            }
            return await dbContext.SaveChangesAsync() > 0;
        }
    }
}
