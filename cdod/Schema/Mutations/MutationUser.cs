using cdod.Schema.InputTypes;
using cdod.Schema.Mutations;
using cdod.Services;
using cdod.Models;

namespace cdod.Schema
{
    [ExtendObjectType(typeof(Mutation))]
    public class MutationUser
    {
       [UseDbContext(typeof(CdodDbContext))]
       public async Task<User> CreateUser(UserInput userForm, [ScopedService] CdodDbContext dbContext)
       {
           User user = new User()
           {
               Email = userForm.Email,
               Password = userForm.Password
           };
           dbContext.Users.Add(user);
           await dbContext.SaveChangesAsync();
           Parent parent = new Parent() { UserId = user.Id };
           dbContext.Parents.Add(parent);
           await dbContext.SaveChangesAsync();
           return user;
       }

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<User> UpdateUser(int id, UserInput userForm, [ScopedService] CdodDbContext dbContext)
        {
            User? user = dbContext.Users.FirstOrDefault(u => u.Id == id);
            if (user == null) throw new Exception("Указанного пользователя не существует");
            user.Firstname = userForm.Firstname ?? user.Firstname;
            user.Lastname = userForm.Lastname ?? user.Lastname;
            user.Patronymic = userForm.Patronymic ?? user.Patronymic;
            user.Address = userForm.Address ?? user.Address;
            user.PhoneNumber = userForm.PhoneNumber ?? user.PhoneNumber;
            user.Email = userForm.Email ?? user.Email;
            user.Password = userForm.Password ?? user.Password;
            user.Birthday = userForm.Birthday ?? user.Birthday;
            user.Education = userForm.Education ?? user.Education;
            user.Inn = userForm.Inn ?? user.Inn;
            user.Snils = userForm.Snils ?? user.Snils;
            user.passportCode = userForm.passportCode ?? user.passportCode;
            user.passportDate = userForm.passportDate ?? user.passportDate;
            user.passportNo = userForm.passportNo ?? user.passportNo;
            user.passportIssue = userForm.passportIssue ?? user.passportIssue;
            dbContext.Users.Update(user);
            await dbContext.SaveChangesAsync();
            return user;
        }

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<bool> DeleteUser(int userId, [ScopedService] CdodDbContext dbContext)
        {
            User? user = dbContext.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null) throw new Exception("Указанного пользователя не существует");
            dbContext.Users.Remove(user);
            return await dbContext.SaveChangesAsync() > 0;
        }
    }
}
