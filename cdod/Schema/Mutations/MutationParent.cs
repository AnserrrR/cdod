using cdod.Models;
using cdod.Schema.InputTypes;
using cdod.Schema.OutputTypes;
using cdod.Services;
using HotChocolate.AspNetCore.Authorization;

namespace cdod.Schema.Mutations
{
    [ExtendObjectType(typeof(Mutation))]
    public class MutationParent
    {

        [Authorize(Roles = new[] { "admin" })]
        [UseDbContext(typeof(CdodDbContext))]
        public async Task<ParentType> ParentCreate(ParentInput  parent, [ScopedService] CdodDbContext dbContext)
        {
            if (dbContext.Users.FirstOrDefault(u => ((u.Email == parent.Email) && (parent.Email != null))) is not null) throw new GraphQLException("Пользователь с таким емейлом уже существует");
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
                // Обработчик на телефон и емейл?
                Parent parentSave = new Parent()
                {
                    UserId = user.Id,
                    SecondEmail = parent.SecondEmail,
                    SecondPhoneNumber = parent.SecondPhoneNumber,
                    SignDate = parent.applyingDate,
                    Type = parent.relationType
                };
                dbContext.Parents.Add(parentSave);
                await dbContext.SaveChangesAsync();
                ParentType parentOutput = new ParentType(user)
                {
                    Id = user.Id,
                    SecondEmail = parent.SecondEmail,
                    SecondPhoneNumber = parent.SecondPhoneNumber,
                    SignDate = parent.applyingDate,
                    Type = parent.relationType
                };

                return parentOutput;
        }

        [Authorize(Roles = new[] { "admin" })]
        [UseDbContext(typeof(CdodDbContext))]
        public async Task<ParentType> ParentUpdate(int id, ParentInput parent, [ScopedService] CdodDbContext dbContext)
        {
            User? _user = dbContext.Users.FirstOrDefault(u => u.Id == id);
            if (_user is null) throw new Exception($"Пользователя с таким ID: {id} - не существует");
            Parent? _parent = dbContext.Parents.FirstOrDefault(p => p.UserId == id);
            if (_user is null) throw new Exception($"Родителя с таким ID: {id} - не существует");
            bool isEmailFree = dbContext.Users.FirstOrDefault(u => ((u.Email == parent.Email) && (u.Id != id))) is null;
            if (!isEmailFree) throw new Exception($"Пользователь с таким емейлом уже существует");
            

            _user.Firstname = parent.Firstname ?? _user.Firstname;
            _user.Lastname = parent.Lastname ?? _user.Lastname;
            _user.Patronymic = parent.Patronymic ?? _user.Patronymic;
            _user.PhoneNumber = parent.PhoneNumber ?? _user.PhoneNumber;
            _user.Email = parent.Email ?? _user.Email;
            _user.Password = parent.Password ?? _user.Password;
            _user.Birthday = parent.Birthday ?? _user.Birthday;
            _user.Address = parent.Address ?? _user.Address;
            _user.Education = parent.Education ?? _user.Education;
            _user.Inn = parent.Inn ?? _user.Inn;
            _user.Snils = parent.Snils ?? _user.Snils;
            _user.passportNo = parent.passportNo ?? _user.passportNo;
            _user.passportIssue = parent.passportIssue ?? _user.passportIssue;
            _user.passportDate = parent.passportDate ?? _user.passportDate;
            _user.passportCode = parent.passportCode ?? _user.passportCode;
            _parent.SecondPhoneNumber = parent.SecondPhoneNumber ?? _parent.SecondPhoneNumber;
            _parent.SecondEmail = parent.SecondEmail ?? _parent.SecondEmail;
            _parent.SignDate = parent.applyingDate ?? _parent.SignDate;
            _parent.Type = parent.relationType ?? _parent.Type;

            ParentType newParent = new ParentType(_user)
            {
                Id = _user.Id,
                SecondEmail = _parent.SecondEmail,
                SecondPhoneNumber = _parent.SecondPhoneNumber,
                Type = _parent.Type
            };
            dbContext.Users.Update(_user);
            dbContext.Parents.Update(_parent);
            await dbContext.SaveChangesAsync();
            return newParent;
        }

        [Authorize(Roles = new[] { "admin" })]
        [UseDbContext(typeof(CdodDbContext))]
        public async Task<bool> ParentDeleteMany(List<int> parentsIds, [ScopedService] CdodDbContext dbContext)
        {
            List<int> errorNotParentIds = new List<int>();
            foreach(int id in parentsIds)
            {
                Parent? parent = dbContext.Parents.FirstOrDefault(p => p.UserId == id);
                if (parent is null) {errorNotParentIds.Add(id); continue;}
                dbContext.Parents.Remove(parent);
            }
            if (errorNotParentIds.Count() > 0) throw new GraphQLException($"Невозможно удалить следующих пользователей:\n" +
    $"ID следующих пользователей не являющихся родителями: {string.Join(" ", errorNotParentIds)}");
            return await dbContext.SaveChangesAsync() > 0;
        }

    }
}
