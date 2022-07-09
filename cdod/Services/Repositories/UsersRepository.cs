using cdod.Models;
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

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            using (CdodDbContext context = _dbContext.CreateDbContext())
            {
                return await context.Users.ToListAsync();
            }
        }

        public async Task<User> GetUserById(int userId)
        {
            // Возмодно сделать обработчик
            using (CdodDbContext context = _dbContext.CreateDbContext())
            {
                return await context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            }
        }

        public async Task<int> CreateUser(User user)
        {
            using (CdodDbContext context = _dbContext.CreateDbContext())
            {
                try
                {
                    context.Users.Add(user);
                    Parent parent = new Parent() { UserId = user.Id};
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

        public async Task<User> UpdateUser(User user)
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
                User user = new User() { Id = userId };
                context.Users.Remove(user);
                return await context.SaveChangesAsync() > 0;
            }
        }
    }
}
