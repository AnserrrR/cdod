using cdod.Schema.InputTypes;
using cdod.Services;
using cdods.s;

namespace cdod.Schema.Mutations
{
    [ExtendObjectType(typeof(Mutation))]
    public class MutationParent
    {
        [UseDbContext(typeof(CdodDbContext))]
        public async Task<Parent> UpdateParent(int id, ParentInput parentForm, [ScopedService] CdodDbContext dBContext)
        { 
                Parent parent = new Parent()
                {
                    UserId = id,
                    SecondEmail = parentForm.SecondEmail,
                    SecondPhoneNumber = parentForm.SecondPhoneNumber,
                    SignDate = parentForm.SignDate
                };
                dBContext.Parents.Update(parent);
                await dBContext.SaveChangesAsync();
                return parent;
        }
    }
}
