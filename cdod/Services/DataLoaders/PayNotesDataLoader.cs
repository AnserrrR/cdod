using cdod.Models;
using cdod.Services.HelperTypes;
using Microsoft.EntityFrameworkCore;

namespace cdod.Services.DataLoaders
{
    public class PayNotesDataLoader : BatchDataLoader<(int, int, Appointment), PayNotesSum>
    {
        private readonly IDbContextFactory<CdodDbContext> _cdodContextFactory;

        public PayNotesDataLoader(
            IDbContextFactory<CdodDbContext> contextFactory,
            IBatchScheduler batchScheduler,
            DataLoaderOptions options = null)
            : base(batchScheduler, options)
        {
            _cdodContextFactory = contextFactory;
        }

        protected override async Task<IReadOnlyDictionary<(int, int, Appointment), PayNotesSum>> 
            LoadBatchAsync(IReadOnlyList<(int, int, Appointment)> keys, CancellationToken cancellationToken)
        {
            using var context = _cdodContextFactory.CreateDbContext();

            var courseIds = keys.Select(k => k.Item1);
            var studentIds = keys.Select(k => k.Item2);
            var appointments = keys.Select(k => k.Item3);

            var query = from pn in context.PayNotes
                where courseIds.Contains(pn.Id) && studentIds.Contains(pn.StudentId) && appointments.Contains(pn.Appointment)
                group pn by new {pn.CourseId, pn.StudentId, pn.Appointment} into res
                select new PayNotesSum()
                {
                    StudentId = res.Key.StudentId,
                    CourseId = res.Key.CourseId,
                    Appointment = res.Key.Appointment,
                    TotalSum = res.Sum(pn => pn.Sum)
                };

            IEnumerable<PayNotesSum> PNSs = from pns in await query.ToListAsync()
                join key in keys on new { pns.CourseId, pns.StudentId, pns.Appointment }
                    equals new { CourseId = key.Item1, StudentId = key.Item2, Appointment = key.Item3 }
                select pns;

            return PNSs.ToDictionary(i => (i.CourseId, i.StudentId, i.Appointment));
        }
    }
}
