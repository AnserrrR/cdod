using cdodDTOs.DTOs;
using Microsoft.EntityFrameworkCore;

namespace cdod.Services.Repositories
{
    public class UsersRepository
    {
        private readonly IDbContextFactory<CdodDbContext> _dbContext;

        public UsersRepository(IDbContextFactory<CdodDbContext> dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            using (CdodDbContext context = _dbContext.CreateDbContext())
            {
                return await context.Users.ToListAsync();
            }
        }

        public async Task<UserDTO> GetUserById(int userId)
        {
            // Возмодно сделать обработчик
            using (CdodDbContext context = _dbContext.CreateDbContext())
            {
                return await context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            }
        }

        public async Task<int> CreateUser(UserDTO user)
        {
            using (CdodDbContext context = _dbContext.CreateDbContext())
            {
                try
                {
                    context.Users.Add(user);
                    ParentDTO parent = new ParentDTO() { UserId = user.Id};
                    context.Parents.Add(parent);
                    await context.SaveChangesAsync();
                    return user.Id;
                }
                catch (Exception ex)
                {
                    return -1;
                }
            }
        }

        public async Task<UserDTO> UpdateUser(UserDTO user)
        {
            using (CdodDbContext context = _dbContext.CreateDbContext())
            {
                context.Users.Update(user);
                await context.SaveChangesAsync();
                return user;
            }
        }

        public async Task<bool> DeleteUser(int userId)
        {
            using (CdodDbContext context = _dbContext.CreateDbContext())
            {
                UserDTO user = new UserDTO() { Id = userId };
                context.Users.Remove(user);
                return await context.SaveChangesAsync() > 0;
            }
        }
    }
}
