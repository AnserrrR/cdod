using cdod.Models;
using cdod.Schema.InputTypes;
using cdod.Schema.OutputTypes;
using cdod.Services;

namespace cdod.Schema.Mutations
{
    [ExtendObjectType(typeof(Mutation))]
    public class MutationGroup
    {
        [UseDbContext(typeof(CdodDbContext))]
        public async Task<GroupType> GroupCreate(GroupCreateInput group, [ScopedService] CdodDbContext dbContext)
        {
            Group _group = new Group()
            {
                Name = group.Name,
                StartDate = group.StartDate,
                TeacherId = group.TeacherId,
                CourseId = group.CourseId
            };

            dbContext.Groups.Add(_group);
            await dbContext.SaveChangesAsync();

            GroupType groupOutput = new GroupType()
            {
                Id = _group.Id,
                StartDate = _group.StartDate,
                TeacherId = _group.TeacherId,
                Name = _group.Name,
                CourseId = _group.CourseId,
            };
            return groupOutput;
        }

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<bool> GroupUpdateMany(List<GroupUpdateInput> groups, [ScopedService] CdodDbContext dbContext)
        {
            List<int> groupErrorIds = new List<int>();
            dbContext.Groups.UpdateRange(groups.Select(g =>
            {
                Group? _group = dbContext.Groups.FirstOrDefault(gid => gid.Id == g.Id);
                if (_group is null) { groupErrorIds.Add(g.Id);  }
                Group group = new Group()
                {
                    Id = g.Id,
                    Name = g.Name ?? _group.Name,
                    StartDate = g.StartDate ?? _group.StartDate,
                    TeacherId = g.TeacherId ?? _group.TeacherId
                };
                return group;
            }));
            
            if(groupErrorIds.Count() > 0)
            {
                throw new GraphQLException($"Не удалось обновить группы со следующим ID: {string.Join(" ", groupErrorIds)}");
            }
            return await dbContext.SaveChangesAsync() > 0;
        }

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<bool> GroupDeleteMany(List<int> groupIds, [ScopedService] CdodDbContext dbContext)
        {
            List<int> groupErrorIds = new List<int>();
            dbContext.Groups.RemoveRange(groupIds.Select(g =>
            {
                Group? _group = dbContext.Groups.FirstOrDefault(gid => gid.Id == g);
                if (_group is null) { groupErrorIds.Add(g);  throw new Exception($"Ошибка произошла туть {g}"); }
                return _group;
            }));

            if (groupErrorIds.Count() > 0)
            {
                throw new GraphQLException($"Не удалось обновить группы со следующим ID: {string.Join(" ", groupErrorIds)}");
            }
            return await dbContext.SaveChangesAsync() > 0;
        }

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<bool> AttachStudentsToGroups(List<StudentToGroupInput> studentsToGroups, [ScopedService] CdodDbContext dbContext)
        {
            dbContext.StudentsToGroups.AddRange(studentsToGroups.Select(stg=>
            {
                var studentTogroup = dbContext.StudentsToGroups.FirstOrDefault(_stg =>
                    ((stg.groupId == _stg.GroupId)&&(stg.studentId == _stg.StudentId)));
                if (studentTogroup is not null) throw new Exception("Ученик уже привязан к группе");
                return studentTogroup;
            }));
            return await dbContext.SaveChangesAsync() > 0;
        }

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<bool> DettachStudentsFromGroups(List<StudentToGroupInput> studentsToGroups, [ScopedService] CdodDbContext dbContext)
        {
            dbContext.StudentsToGroups.RemoveRange(studentsToGroups.Select(stc =>
            {
                var studentToGroup = dbContext.StudentsToGroups.FirstOrDefault(_stc =>
                ((stc.groupId == _stc.GroupId) && (stc.studentId == _stc.StudentId)));
                if (studentToGroup == null) throw new Exception($"Студент и так не привязан к группе:{stc.studentId}");
                return studentToGroup;
            }));
            return await dbContext.SaveChangesAsync() > 0;
        }
    }
}
