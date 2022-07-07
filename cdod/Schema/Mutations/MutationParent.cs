using cdod.Schema.InputTypes;
using cdod.Services;
using cdodDTOs.DTOs;

namespace cdod.Schema.Mutations
{
    [ExtendObjectType(typeof(Mutation))]
    public class MutationParent
    {
        [UseDbContext(typeof(CdodDbContext))]
        public async Task<ParentDTO> UpdateParent(int id, ParentInput parentForm, [ScopedService] CdodDbContext dBContext)
        { 
                ParentDTO parent = new ParentDTO()
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
