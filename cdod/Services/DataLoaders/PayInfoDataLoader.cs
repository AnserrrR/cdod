using cdod.Schema.OutputTypes;
using cdod.Services.HelperTypes;
using Microsoft.EntityFrameworkCore;

namespace cdod.Services.DataLoaders
{
    public class PayInfoDataLoader : BatchDataLoader<(int, int), PayInfo>
    {
        private readonly IDbContextFactory<CdodDbContext> _cdodContextFactory;

        public PayInfoDataLoader(
            IDbContextFactory<CdodDbContext> contextFactory,
            IBatchScheduler batchScheduler,
            DataLoaderOptions options = null)
            : base(batchScheduler, options)
        {
            _cdodContextFactory = contextFactory;
        }

        protected override async Task<IReadOnlyDictionary<(int, int), PayInfo>> LoadBatchAsync(
            IReadOnlyList<(int, int)> keys, CancellationToken cancellationToken)
        {
            using var context = _cdodContextFactory.CreateDbContext();

            var courseIds = keys.Select(k => k.Item1);
            var studentIds = keys.Select(k => k.Item2);

            var query = from stc in context.StudentToCourses
                join c in context.Courses on stc.CourseId equals c.Id
                join cs in context.ContractStates on stc.ContractStateId equals cs.Id
                where courseIds.Contains(c.Id) && studentIds.Contains(stc.StudentId)
                select new PayInfo()
                {
                    CourseId = stc.CourseId,
                    StudentId = stc.StudentId,
                    ContractState = cs.Name,
                    CoursePrice = c.CoursePrice,
                    CourseName = c.Name,
                    DurationInMonths = c.DurationInMonths,
                    SignDate = stc.SignDate,
                    IsEquipmentPriceWithRobot = stc.EquipmentPriceWithRobot,
                    EquipmentPriceWithRobot = c.EquipmentPriceWithRobot,
                    EquipmentPriceWithoutRobot = c.EquipmentPriceWithoutRobot,
                };

            IEnumerable<PayInfo> PIs =  from pi in await query.ToListAsync()
                join key in keys on new { pi.CourseId, pi.StudentId, }
                    equals new { CourseId = key.Item1, StudentId = key.Item2 }
                select pi;

            return PIs.ToDictionary(i => (i.CourseId, i.StudentId));

        }
    }
}
