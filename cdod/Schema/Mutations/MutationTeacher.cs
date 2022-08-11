//using cdod.Models;
//using cdod.Schema.InputTypes;
//using cdod.Schema.OutputTypes;
//using cdod.Services;

//namespace cdod.Schema.Mutations
//{
//    [ExtendObjectType(typeof(Mutation))]
//    public class MutationTeacher
//    {
//        [UseDbContext(typeof(CdodDbContext))]
//        public async Task<TeacherType> TeacherCreate(TeacherInput teacher, [ScopedService] CdodDbContext dbContext)
//        {
//            if (dbContext.Users.FirstOrDefault(u => u.Email == teacher.Email) is not null) throw new GraphQLException("Пользователь с таким емейлом уже существует");
//            User user = new User()
//            {
//                Firstname = teacher.Firstname,
//                Lastname = teacher.Firstname,
//                Patronymic = teacher.Firstname,
//                PhoneNumber = teacher.PhoneNumber,
//                Email = teacher.Email,
//                Password = teacher.Password,
//                Birthday = teacher.Birthday,
//                Address = teacher.Address,
//                Education = teacher.Education,
//                Inn = teacher.Inn,
//                Snils = teacher.Snils,
//                passportNo = teacher.passportNo,
//                passportIssue = teacher.passportIssue,
//                passportDate = teacher.passportDate,
//                passportCode = teacher.passportCode,
//            };
//            dbContext.Users.Add(user);
//            await dbContext.SaveChangesAsync();
//            Teacher teacherSave = new Teacher()
//            {
//                UserId = user.Id,
//                WorkPlace = teacher.WorkPlace,
//                PostId = teacher.PostId
//            };
//            dbContext.Teachers.Add(teacherSave);
//            await dbContext.SaveChangesAsync();
//            TeacherType patentOutput = new TeacherType(user)
//            {
//                Id = user.Id,
//                WorkPlace = teacher.WorkPlace,
//                PostId = teacher.PostId
//            };

//            return patentOutput;
//        }

//        [UseDbContext(typeof(CdodDbContext))]
//        public async Task<bool> TeacherUpdateMany(List<TeacherInput> teacher, [ScopedService] CdodDbContext dbContext)
//        {
//            List<int> errorUserIds = new List<int>();
//            List<int> errorNotTeacherIds = new List<int>();
//            List<User> userUpdated = new List<User>();
//            List<Teacher> teacherUpdated = new List<Teacher>();
//            foreach (TeacherInput el in teacher)
//            {
//                User? _user = dbContext.Users.FirstOrDefault(u => u.Id == el.Id);
//                if (_user == null) { errorUserIds.Add(el.Id); continue; }
//                Teacher? _teacher = dbContext.Teachers.FirstOrDefault(t => t.UserId == el.Id);
//                if (_teacher is null) { errorNotTeacherIds.Add(el.Id); continue; }
//                _user.Firstname = el.Firstname ?? _user.Firstname;
//                _user.Lastname = el.Lastname ?? _user.Lastname;
//                _user.Patronymic = el.Patronymic ?? _user.Patronymic;
//                _user.PhoneNumber = el.PhoneNumber ?? _user.PhoneNumber;
//                _user.Email = el.Email ?? _user.Email;
//                _user.Password = el.Password ?? _user.Password;
//                _user.Birthday = el.Birthday ?? _user.Birthday;
//                _user.Address = el.Address ?? _user.Address;
//                _user.Education = el.Education ?? _user.Education;
//                _user.Inn = el.Inn ?? _user.Inn;
//                _user.Snils = el.Snils ?? _user.Snils;
//                _user.passportNo = el.passportNo ?? _user.passportNo;
//                _user.passportIssue = el.passportIssue ?? _user.passportIssue;
//                _user.passportDate = el.passportDate ?? _user.passportDate;
//                _user.passportCode = el.passportCode ?? _user.passportCode;
//                _teacher.WorkPlace = el.WorkPlace ?? _teacher.WorkPlace;
//                _teacher.PostId = el.PostId ?? _teacher.PostId;

//                userUpdated.Add(_user);
//                teacherUpdated.Add(_teacher);
//            }
//            dbContext.Users.UpdateRange(userUpdated);
//            dbContext.Teachers.UpdateRange(teacherUpdated);
//            if (errorUserIds.Count() > 0 || errorNotTeacherIds.Count() > 0)
//            {
//                if (errorUserIds.Count() > 0 && errorNotTeacherIds.Count > 0)
//                {
//                    throw new GraphQLException($"Невозможно обновить следующих пользователей:\n" +
//                        $"ID следующих пользователей нет в системе: {string.Join(" ", errorUserIds)}\n" +
//                        $"ID следующих пользователей не являющихся Учителями: {string.Join(" ", errorNotTeacherIds)}");
//                }
//                else if (errorUserIds.Count() > 0)
//                {
//                    throw new GraphQLException($"Невозможно обновить следующих пользователей:\n" +
//                        $"ID следующих пользователей нет в системе: {string.Join(" ", errorUserIds)}\n");
//                }
//                else
//                {
//                    throw new GraphQLException($"Невозможно обновить следующих пользователей:\n" +
//                        $"ID следующих пользователей не являющихся учителями: {string.Join(" ", errorNotTeacherIds)}");

//                }
//            }
//            return await dbContext.SaveChangesAsync() > 0;
//        }

//        [UseDbContext(typeof(CdodDbContext))]
//        public async Task<bool> TeacherDeleteMany(List<int> teacherIds, [ScopedService] CdodDbContext dbContext)
//        {
//            List<int> errorNotTeacherIds = new List<int>();
//            try
//            {
//                dbContext.Teachers.RemoveRange(teacherIds.Select(t =>
//                {
//                    Teacher? _teacher = dbContext.Teachers.FirstOrDefault(tid => tid.UserId == t);
//                    if (_teacher is null)
//                    {
//                        errorNotTeacherIds.Add(t);
//                        throw new GraphQLException($"Невозможно удалить следующих пользователей:\n" +
//                            $"ID следующих пользователей не являющихся родителями: {t}");
//                    }
//                    return _teacher;
//                }));
//            }
//            catch
//            {
//                throw new GraphQLException($"Невозможно удалить следующих пользователей:\n" +
//    $"ID следующих пользователей не являющихся родителями: {string.Join(" ", errorNotTeacherIds)}");

//            }
//            return await dbContext.SaveChangesAsync() > 0;
//        }
//    }
//}
