using cdod.Models;
using Microsoft.EntityFrameworkCore;

namespace cdod.Services.Repositories
{
    public class ParentsRepository
    {
        private readonly IDbContextFactory<CdodDbContext> _dbContext;
        
        ParentsRepository(IDbContextFactory<CdodDbContext> dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Parent>> GetAllParents()
        {
            using (CdodDbContext context = _dbContext.CreateDbContext())
            {
                return await context.Parents.ToListAsync();
            }
        }

        public async Task<Parent> GetParentById(int parentId)
        {
            using (CdodDbContext context = _dbContext.CreateDbContext())
            {
                return await context.Parents.FirstOrDefaultAsync(u => u.UserId == parentId);
            }
        }

        public async Task<Parent> UpdateParent(Parent parent)
        {
            using (CdodDbContext context = _dbContext.CreateDbContext())
            {
                context.Parents.Update(parent);
                await context.SaveChangesAsync();
                return parent;
            }
        }
    }
}
