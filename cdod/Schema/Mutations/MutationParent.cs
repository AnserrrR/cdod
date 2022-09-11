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
                    Firstname = parent.FirstName,
                    Lastname = parent.FirstName,
                    Patronymic = parent.FirstName,
                    PhoneNumber = parent.PhoneNumber,
                    Email = parent.Email,
                    Password = parent.Password,
                    Birthday = parent.Birthday,
                    Address = parent.Address,
                    Education = parent.Education,
                    Inn = parent.Inn,
                    Snils = parent.Snils,
                    passportNo = parent.PassportNo,
                    passportIssue = parent.PassportIssue,
                    passportDate = parent.PassportDate,
                    passportCode = parent.PassportCode,
                };
                dbContext.Users.Add(user);
                await dbContext.SaveChangesAsync();
                // Обработчик на телефон и емейл?
                Parent parentSave = new Parent()
                {
                    UserId = user.Id,
                    SecondEmail = parent.SecondEmail,
                    SecondPhoneNumber = parent.SecondPhoneNumber,
                    SignDate = parent.ApplyingDate,
                    Type = parent.RelationType
                };
                dbContext.Parents.Add(parentSave);
                await dbContext.SaveChangesAsync();
                ParentType parentOutput = new ParentType(user)
                {
                    Id = user.Id,
                    SecondEmail = parent.SecondEmail,
                    SecondPhoneNumber = parent.SecondPhoneNumber,
                    SignDate = parent.ApplyingDate,
                    Type = parent.RelationType
                };

                return parentOutput;
        }

        [Authorize(Roles = new[] { "admin" })]
        [UseDbContext(typeof(CdodDbContext))]
        public async Task<ParentType> ParentUpdate(int id, ParentInput parent, [ScopedService] CdodDbContext dbContext)
        {
            User? userToUpdate = dbContext.Users.FirstOrDefault(u => u.Id == id);
            if (userToUpdate is null) throw new Exception($"Пользователя с таким ID: {id} - не существует");
            Parent? parentToUpdate = dbContext.Parents.FirstOrDefault(p => p.UserId == id);
            if (parentToUpdate is null) throw new Exception($"Родителя с таким ID: {id} - не существует");
            bool isEmailFree = dbContext.Users.FirstOrDefault(u => ((u.Email == parent.Email) && (u.Id != id))) is null;
            if (!isEmailFree) throw new Exception($"Пользователь с таким емейлом уже существует");
            

            userToUpdate.Firstname = parent.FirstName ?? userToUpdate.Firstname;
            userToUpdate.Lastname = parent.LastName ?? userToUpdate.Lastname;
            userToUpdate.Patronymic = parent.Patronymic ?? userToUpdate.Patronymic;
            userToUpdate.PhoneNumber = parent.PhoneNumber ?? userToUpdate.PhoneNumber;
            userToUpdate.Email = parent.Email ?? userToUpdate.Email;
            userToUpdate.Password = parent.Password ?? userToUpdate.Password;
            userToUpdate.Birthday = parent.Birthday ?? userToUpdate.Birthday;
            userToUpdate.Address = parent.Address ?? userToUpdate.Address;
            userToUpdate.Education = parent.Education ?? userToUpdate.Education;
            userToUpdate.Inn = parent.Inn ?? userToUpdate.Inn;
            userToUpdate.Snils = parent.Snils ?? userToUpdate.Snils;
            userToUpdate.passportNo = parent.PassportNo ?? userToUpdate.passportNo;
            userToUpdate.passportIssue = parent.PassportIssue ?? userToUpdate.passportIssue;
            userToUpdate.passportDate = parent.PassportDate ?? userToUpdate.passportDate;
            userToUpdate.passportCode = parent.PassportCode ?? userToUpdate.passportCode;
            parentToUpdate.SecondPhoneNumber = parent.SecondPhoneNumber ?? parentToUpdate.SecondPhoneNumber;
            parentToUpdate.SecondEmail = parent.SecondEmail ?? parentToUpdate.SecondEmail;
            parentToUpdate.SignDate = parent.ApplyingDate ?? parentToUpdate.SignDate;
            parentToUpdate.Type = parent.RelationType ?? parentToUpdate.Type;

            ParentType newParent = new ParentType(userToUpdate)
            {
                Id = userToUpdate.Id,
                SecondEmail = parentToUpdate.SecondEmail,
                SecondPhoneNumber = parentToUpdate.SecondPhoneNumber,
                Type = parentToUpdate.Type
            };
            dbContext.Users.Update(userToUpdate);
            dbContext.Parents.Update(parentToUpdate);
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
