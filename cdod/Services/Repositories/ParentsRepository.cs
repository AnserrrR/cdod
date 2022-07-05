using cdodDTOs.DTOs;
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

        public async Task<IEnumerable<ParentDTO>> GetAllParents()
        {
            using (CdodDbContext context = _dbContext.CreateDbContext())
            {
                return await context.Parents.ToListAsync();
            }
        }

        public async Task<ParentDTO> GetParentById(int parentId)
        {
            using (CdodDbContext context = _dbContext.CreateDbContext())
            {
                return await context.Parents.FirstOrDefaultAsync(u => u.UserId == parentId);
            }
        }

        public async Task<ParentDTO> UpdateParent(ParentDTO parentDTO)
        {
            using (CdodDbContext context = _dbContext.CreateDbContext())
            {
                context.Parents.Update(parentDTO);
                await context.SaveChangesAsync();
                return parentDTO;
            }
        }
    }
}
