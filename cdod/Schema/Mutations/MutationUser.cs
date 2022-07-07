using cdod.Schema.InputTypes;
using cdod.Schema.Mutations;
using cdod.Services;
using cdods.s;

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
                        Firstname = userForm.Firstname,
                        Lastname = userForm.Lastname,
                        Patronymic = userForm.Patronymic,
                        Address = userForm.Address,
                        PhoneNumber = userForm.PhoneNumber,
                        Email = userForm.Email,
                        Password = userForm.Password,
                        Birthday = userForm.Birthday,
                        Education = userForm.Education,
                        Inn = userForm.Inn,
                        Snils = userForm.Snils,
                        passportCode = userForm.passportCode,
                        passportDate = userForm.passportDate,
                        passportNo = userForm.passportNo,
                        passportIssue = userForm.passportIssue,
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
            User user = new User()
            {
                Id = id,
                Firstname = userForm.Firstname,
                Lastname = userForm.Lastname,
                Patronymic = userForm.Patronymic,
                Address = userForm.Address,
                PhoneNumber = userForm.PhoneNumber,
                Email = userForm.Email,
                Password = userForm.Password,
                Birthday = userForm.Birthday,
                Education = userForm.Education,
                Inn = userForm.Inn,
                Snils = userForm.Snils,
                passportCode = userForm.passportCode,
                passportDate = userForm.passportDate,
                passportNo = userForm.passportNo,
                passportIssue = userForm.passportIssue,
            };
            dbContext.Users.Update(user);
            await dbContext.SaveChangesAsync();
            return user;
        }

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<bool> DeleteUser(int userId, [ScopedService] CdodDbContext dbContext)
        {
            User user = new User() { Id = userId };
            dbContext.Users.Remove(user);
            return await dbContext.SaveChangesAsync() > 0;
        }
    }
}
