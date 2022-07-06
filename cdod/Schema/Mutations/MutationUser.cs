using cdod.Schema.InputTypes;
using cdod.Services;
using cdodDTOs.DTOs;

namespace cdod.Schema
{
    public class MutationUser
    {
       [UseDbContext(typeof(CdodDbContext))]
       public async Task<UserDTO> CreateUser(UserInput userForm, [Service] CdodDbContext dbContext)
       {
            UserDTO user = new UserDTO()
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
            ParentDTO parent = new ParentDTO() { UserId = user.Id };
            dbContext.Parents.Add(parent);
            await dbContext.SaveChangesAsync();
            return user;
        }

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<UserDTO> UpdateUser(int id, UserInput userForm, [Service] CdodDbContext dbContext)
        {
            UserDTO user = new UserDTO()
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
        public async Task<bool> DeleteUser(int userId, [Service] CdodDbContext dbContext)
        {
            UserDTO user = new UserDTO() { Id = userId };
            dbContext.Users.Remove(user);
            return await dbContext.SaveChangesAsync() > 0;
        }
    }
}
