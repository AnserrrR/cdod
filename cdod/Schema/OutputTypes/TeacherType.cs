using System.ComponentModel.DataAnnotations.Schema;
using cdod.Models;
using cdod.Services.DataLoaders;

namespace cdod.Schema.OutputTypes
{
    public class TeacherType
    {
        [IsProjected]
        public int UserId { get; set; }

        public async Task<User> User([Service] UserDataLoader userDataLoader)
        {
            var user = await userDataLoader.LoadAsync(UserId);
            return user;
        }

        public string WorkPlace { get; set; }

        public int PostId { get; set; }

        public async Task<string> Post([Service] PostDataLoader postDataLoader)
        {
            var post = await postDataLoader.LoadAsync(PostId);
            return post.Name;
        }
    }
}
