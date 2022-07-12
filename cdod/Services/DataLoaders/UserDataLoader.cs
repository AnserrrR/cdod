using cdod.Models;
using Microsoft.EntityFrameworkCore;

namespace cdod.Services.DataLoaders
{
    public class UserDataLoader : BatchDataLoader<int, User>
    {
        private readonly IDbContextFactory<CdodDbContext> _cdodContextFactory;

        public UserDataLoader(
            IDbContextFactory<CdodDbContext> contextFactory,
            IBatchScheduler batchScheduler,
            DataLoaderOptions options = null)
            : base(batchScheduler, options)
        {
            _cdodContextFactory = contextFactory;
        }

        protected override async Task<IReadOnlyDictionary<int, User>> LoadBatchAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken)
        {
            using var context = _cdodContextFactory.CreateDbContext();

            IEnumerable<User> users = await context.Users
                .Where(i => keys.Contains(i.Id))
                .ToListAsync();

            return users.ToDictionary(i => i.Id);
        }
    }
}
