using cdod.Schema.InputTypes;
using cdod.Services;
using cdod.Models;
using cdod.Schema.OutputTypes;
using HotChocolate.AspNetCore.Authorization;

namespace cdod.Schema.Mutations
{
    [ExtendObjectType(typeof(Mutation))]
    public class MutationPost
    {
        [Authorize(Roles = new[] { "admin" })]
        [UseDbContext(typeof(CdodDbContext))]
        public async Task<PostType> CreatePost(PostCreateInput post, [ScopedService] CdodDbContext dbContext)
        {
            Post postToCreate = new Post()
            {
                Name = post.Name
            };
            dbContext.Posts.Add(postToCreate);
            await dbContext.SaveChangesAsync();
            PostType postOutput = new PostType()
            {
                Id = postToCreate.Id,
                Name = postToCreate.Name
            };
            return postOutput;
        }

        [Authorize(Roles = new[] { "admin" })]
        [UseDbContext(typeof(CdodDbContext))]
        public async Task<bool> PostUpdateMany(List<PostUpdateInput> posts, [ScopedService] CdodDbContext dbContext)
        {
            List<int> errorPostIds = new List<int>();
            List<Post> postUpdated = new List<Post>();
            foreach (var post in posts)
            {
                Post? postToUpdate = dbContext.Posts.FirstOrDefault(s => s.Id == post.Id);
                if (postToUpdate == null) { errorPostIds.Add(post.Id); continue; };

                postToUpdate.Name = post.Name ?? postToUpdate.Name;
                postUpdated.Add(postToUpdate);
            }

            dbContext.Posts.UpdateRange(postUpdated);

            return await dbContext.SaveChangesAsync() > 0;
        }

        [Authorize(Roles = new[] { "admin" })]
        [UseDbContext(typeof(CdodDbContext))]
        public async Task<bool> SchoolDeleteMany(List<int> postIds, [ScopedService] CdodDbContext dbContext)
        {
            List<int> errorNotpostIds = new List<int>();
            foreach (int id in postIds)
            {
                Post? post = dbContext.Posts.FirstOrDefault(p => p.Id == id);
                if (post is null) { errorNotpostIds.Add(id); continue; }
                dbContext.Posts.Remove(post);
            }
            if (errorNotpostIds.Count() > 0) throw new GraphQLException($"Невозможно удалить следующие посты:\n" +
    $": {string.Join(" ", errorNotpostIds)}");
            return await dbContext.SaveChangesAsync() > 0;
        }
    }
}
