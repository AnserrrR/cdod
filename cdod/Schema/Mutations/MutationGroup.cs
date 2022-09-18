using cdod.Models;
using cdod.Schema.InputTypes;
using cdod.Schema.OutputTypes;
using cdod.Services;
using HotChocolate.AspNetCore.Authorization;

namespace cdod.Schema.Mutations
{
    [ExtendObjectType(typeof(Mutation))]
    public class MutationGroup
    {
        [Authorize(Roles = new[] { "admin" })]
        [UseDbContext(typeof(CdodDbContext))]
        public async Task<GroupType> GroupCreate(GroupCreateInput group, [ScopedService] CdodDbContext dbContext)
        {
            Group groupToCreate = new Group()
            {
                Name = group.Name,
                StartDate = group.StartDate,
                TeacherId = group.TeacherId,
                CourseId = group.CourseId
            };

            dbContext.Groups.Add(groupToCreate);
            await dbContext.SaveChangesAsync();

            GroupType groupOutput = new GroupType()
            {
                Id = groupToCreate.Id,
                StartDate = groupToCreate.StartDate,
                TeacherId = groupToCreate.TeacherId,
                Name = groupToCreate.Name,
                CourseId = groupToCreate.CourseId,
            };
            return groupOutput;
        }

        [Authorize(Roles = new[] { "admin" })]
        [UseDbContext(typeof(CdodDbContext))]
        public async Task<bool> GroupUpdateMany(List<GroupUpdateInput> groups, [ScopedService] CdodDbContext dbContext)
        {
            List<int> groupErrorIds = new List<int>();
            dbContext.Groups.UpdateRange(groups.Select(g =>
            {
                Group? group = dbContext.Groups.FirstOrDefault(gid => gid.Id == g.Id);
                if (group is null) { groupErrorIds.Add(g.Id); }
                group.Name = g.Name ?? group.Name;
                group.StartDate = g.StartDate ?? group.StartDate;
                group.TeacherId = g.TeacherId ?? group.TeacherId;
                return group;
            }));

            if (groupErrorIds.Count() > 0)
            {
                throw new GraphQLException($"Не удалось обновить группы со следующим ID: {string.Join(" ", groupErrorIds)}");
            }
            return await dbContext.SaveChangesAsync() > 0;
        }

        [Authorize(Roles = new[] { "admin" })]
        [UseDbContext(typeof(CdodDbContext))]
        public async Task<bool> GroupDeleteMany(List<int> groupIds, [ScopedService] CdodDbContext dbContext)
        {
            List<int> groupErrorIds = new List<int>();
            dbContext.Groups.RemoveRange(groupIds.Select(g =>
            {
                Group? groupToDelete = dbContext.Groups.FirstOrDefault(gid => gid.Id == g);
                if (groupToDelete is null) { groupErrorIds.Add(g); throw new Exception($"Ошибка произошла туть {g}"); }
                return groupToDelete;
            }));

            if (groupErrorIds.Count() > 0)
            {
                throw new GraphQLException($"Не удалось обновить группы со следующим ID: {string.Join(" ", groupErrorIds)}");
            }
            return await dbContext.SaveChangesAsync() > 0;
        }
    }
}
