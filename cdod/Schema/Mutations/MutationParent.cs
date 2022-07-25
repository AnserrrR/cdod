using cdod.Models;
using cdod.Schema.InputTypes;
using cdod.Schema.OutputTypes;
using cdod.Services;

namespace cdod.Schema.Mutations
{
    [ExtendObjectType(typeof(Mutation))]
    public class MutationParent
    {

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<ParentType> ParentCreate(ParentCreateInput parent, [ScopedService] CdodDbContext dbContext)
        { // throw new GraphQLException("Пользователь с таким емейлом уже существует")
            //if (dbContext.Users.FirstOrDefault(u => u.Email == parent.Email) is not null) throw new GraphQLException("Пользователь с таким емейлом уже существует");
            try
            {
                User user = new User()
                {
                    Firstname = parent.Firstname,
                    Lastname = parent.Firstname,
                    Patronymic = parent.Firstname,
                    PhoneNumber = parent.PhoneNumber,
                    Email = parent.Email,
                    Password = parent.Password,
                    Birthday = parent.Birthday,
                    Address = parent.Address,
                    Education = parent.Education,
                    Inn = parent.Inn,
                    Snils = parent.Snils,
                    passportNo = parent.passportNo,
                    passportIssue = parent.passportIssue,
                    passportDate = parent.passportDate,
                    passportCode = parent.passportCode,
                };
                dbContext.Users.Add(user);
                await dbContext.SaveChangesAsync();
                Parent parentSave = new Parent()
                {
                    UserId = user.Id,
                    SecondEmail = parent.SecondEmail,
                    SecondPhoneNumber = parent.SecondPhoneNumber,
                    SignDate = parent.applyingDate
                };
                dbContext.Parents.Add(parentSave);
                await dbContext.SaveChangesAsync();
                ParentType parentOutput = new ParentType(user)
                {
                    Id = user.Id,
                    SecondEmail = parent.SecondEmail,
                    SecondPhoneNumber = parent.SecondPhoneNumber,
                    SignDate = parent.applyingDate
                };

                return parentOutput;
            }
            catch (Exception ex)
            {
                throw new GraphQLException(ex.ToString());
            }

        }

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<bool> ParentUpdateMany(List<ParentUpdateInput> parent, [ScopedService] CdodDbContext dbContext)
        {
            List<int> errorUserIds = new List<int>();
            List<int> errorNotParentIds = new List<int>();
            List<User> userUpdated = new List<User>();
            List<Parent> parentUpdated = new List<Parent>();
            foreach (ParentUpdateInput el in parent)
            {
                User? _user = dbContext.Users.FirstOrDefault(u => u.Id == el.Id);
                if (_user == null) { errorUserIds.Add(el.Id); continue; }
                Parent? _parent = dbContext.Parents.FirstOrDefault(p => p.UserId == el.Id);
                if (_parent is null) { errorNotParentIds.Add(el.Id); continue; }
                _user.Firstname = el.Firstname ?? _user.Firstname;
                _user.Lastname = el.Lastname ?? _user.Lastname;
                _user.Patronymic = el.Patronymic ?? _user.Patronymic;
                _user.PhoneNumber = el.PhoneNumber ?? _user.PhoneNumber;
                _user.Email = el.Email ?? _user.Email;
                _user.Password = el.Password ?? _user.Password;
                _user.Birthday = el.Birthday ?? _user.Birthday;
                _user.Address = el.Address ?? _user.Address;
                _user.Education = el.Education ?? _user.Education;
                _user.Inn = el.Inn ?? _user.Inn;
                _user.Snils = el.Snils ?? _user.Snils;
                _user.passportNo = el.passportNo ?? _user.passportNo;
                _user.passportIssue = el.passportIssue ?? _user.passportIssue;
                _user.passportDate = el.passportDate ?? _user.passportDate;
                _user.passportCode = el.passportCode ?? _user.passportCode;
                _parent.SecondPhoneNumber = el.SecondPhoneNumber ?? _parent.SecondPhoneNumber;
                _parent.SecondEmail = el.SecondEmail ?? _parent.SecondEmail;
                _parent.SignDate = el.applyingDate ?? _parent.SignDate;
                userUpdated.Add(_user);
                parentUpdated.Add(_parent);
            }
            dbContext.Users.UpdateRange(userUpdated);
            dbContext.Parents.UpdateRange(parentUpdated);
            if (errorUserIds.Count() > 0 || errorNotParentIds.Count() > 0)
            {
                if (errorUserIds.Count() > 0 && errorNotParentIds.Count > 0)
                {
                    throw new GraphQLException($"Невозможно обновить следующих пользователей:\n" +
                        $"ID следующих пользователей нет в системе: {string.Join(" ", errorUserIds)}\n" +
                        $"ID следующих пользователей не являющихся родителями: {string.Join(" ", errorNotParentIds)}");
                }
                else if (errorUserIds.Count() > 0)
                {
                    throw new GraphQLException($"Невозможно обновить следующих пользователей:\n" +
                        $"ID следующих пользователей нет в системе: {string.Join(" ", errorUserIds)}\n");
                }
                else
                {
                    throw new GraphQLException($"Невозможно обновить следующих пользователей:\n" +
                        $"ID следующих пользователей не являющихся родителями: {string.Join(" ", errorNotParentIds)}");

                }
            }
            return await dbContext.SaveChangesAsync() > 0;
        }

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<bool> ParentDeleteMany(List<int> parentsIds, [ScopedService] CdodDbContext dbContext)
        {
            List<int> errorNotParentIds = new List<int>();
            try
            {
                dbContext.Parents.RemoveRange(parentsIds.Select(p =>
                {
                    Parent? _parent = dbContext.Parents.FirstOrDefault(pid => pid.UserId == p);
                    if (_parent is null)
                    {
                        errorNotParentIds.Add(p);
                        throw new GraphQLException($"Невозможно удалить следующих пользователей:\n" +
                            $"ID следующих пользователей не являющихся родителями: {p}");
                    }
                    return _parent;
                }));
            }
            catch
            {
                throw new GraphQLException($"Невозможно удалить следующих пользователей:\n" +
    $"ID следующих пользователей не являющихся родителями: {string.Join(" ", errorNotParentIds)}");

            }
            return await dbContext.SaveChangesAsync() > 0;
        }

    }
}
