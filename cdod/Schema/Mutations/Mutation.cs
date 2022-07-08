using cdod.Services;
using Google.Apis.Sheets;
namespace cdod.Schema.Mutations
{
    public class Mutation
    {
        [UseDbContext(typeof(CdodDbContext))]
        public async void CreateTrialUsersFromGoogleTable()
        {

        }
    }
}
