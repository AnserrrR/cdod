using cdod.Models;
using Microsoft.EntityFrameworkCore;

namespace cdod.Services.DataLoaders
{
    public class ContractStateDataLoader : BatchDataLoader<int, ContractState>
    {
        private readonly IDbContextFactory<CdodDbContext> _cdodContextFactory;

        public ContractStateDataLoader(
            IDbContextFactory<CdodDbContext> contextFactory,
            IBatchScheduler batchScheduler,
            DataLoaderOptions options = null)
            : base(batchScheduler, options)
        {
            _cdodContextFactory = contextFactory;
        }

        protected override async Task<IReadOnlyDictionary<int, ContractState>> LoadBatchAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken)
        {
            using var context = _cdodContextFactory.CreateDbContext();

            IEnumerable<ContractState> contractStates = await context.ContractStates
                .Where(i => keys.Contains(i.Id))
                .ToListAsync();

            return contractStates.ToDictionary(i => i.Id);
        }
    }
}
