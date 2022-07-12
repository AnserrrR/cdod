using cdod.Models;
using Microsoft.EntityFrameworkCore;

namespace cdod.Services.DataLoaders
{
    public class PostDataLoader : BatchDataLoader<int, Post>
    {
    private readonly IDbContextFactory<CdodDbContext> _cdodContextFactory;

    public PostDataLoader(
        IDbContextFactory<CdodDbContext> contextFactory,
        IBatchScheduler batchScheduler,
        DataLoaderOptions options = null)
        : base(batchScheduler, options)
    {
        _cdodContextFactory = contextFactory;
    }

    protected override async Task<IReadOnlyDictionary<int, Post>> LoadBatchAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken)
    {
        using var context = _cdodContextFactory.CreateDbContext();

        IEnumerable<Post> contractStates = await context.Posts
            .Where(i => keys.Contains(i.Id))
            .ToListAsync();

        return contractStates.ToDictionary(i => i.Id);
    }
    }
}
