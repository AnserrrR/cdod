using cdods.s;
using Microsoft.EntityFrameworkCore;

namespace cdod.Services.Repositories
{
    public class PayNotesRepository
    {
        private readonly IDbContextFactory<CdodDbContext> _dbContext;

        public PayNotesRepository(IDbContextFactory<CdodDbContext> dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<PayNote>> GetAllUsers()
        {
            using (CdodDbContext context = _dbContext.CreateDbContext())
            {
                return await context.PayNotes.ToListAsync();
            }
        }

        public async Task<IEnumerable<PayNote>> GetManyPayNotesById(IReadOnlyList<int> payNoteIds)
        {
            using (CdodDbContext context = _dbContext.CreateDbContext())
            {
                return await context.PayNotes.Where(i => payNoteIds.Contains(i.Id)).ToListAsync();
            }
        }

        public async Task<PayNote> GetPayNoteById(int id)
        {
            using (CdodDbContext context = _dbContext.CreateDbContext())
            {
                return await context.PayNotes.FirstOrDefaultAsync(i => i.Id == id);
            }
        }

        public async Task<PayNote> CreatePayNote(PayNote payNote)
        {
            using (CdodDbContext context = _dbContext.CreateDbContext())
            {
                context.PayNotes.Add(payNote);
                await context.SaveChangesAsync();
                return payNote;
            }
        }

        public async Task<PayNote> PayNote(PayNote payNote)
        {
            using (CdodDbContext context = _dbContext.CreateDbContext())
            {
                context.PayNotes.Update(payNote);
                await context.SaveChangesAsync();
                return payNote;
            }
        }

        public async Task<bool> DeletePayNote(int id)
        {
            using (CdodDbContext context = _dbContext.CreateDbContext())
            {
                PayNote payNote = new PayNote() { Id = id};
                context.PayNotes.Remove(payNote);
                return await context.SaveChangesAsync() > 0;
                 
            }
        }
    }
}
