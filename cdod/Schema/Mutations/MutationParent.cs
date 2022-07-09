using cdod.Schema.InputTypes;
using cdod.Services;
using cdod.Models;

namespace cdod.Schema.Mutations
{
    [ExtendObjectType(typeof(Mutation))]
    public class MutationParent
    {
        [UseDbContext(typeof(CdodDbContext))]
        public async Task<Parent> UpdateParent(int id, ParentInput parentForm, [ScopedService] CdodDbContext dBContext)
        {
            Parent? parent = dBContext.Parents.FirstOrDefault(p => p.UserId == id);
            if (parent == null) throw new Exception("Указанного родителя не существует");

            parent.SecondEmail = parentForm.SecondEmail ?? parent.SecondEmail;
            parent.SecondPhoneNumber = parentForm.SecondPhoneNumber ?? parent.SecondPhoneNumber;
            parent.SignDate = parentForm.SignDate ?? parent.SignDate;
            dBContext.Parents.Update(parent);
            await dBContext.SaveChangesAsync();
            return parent;
        }
    }
}
