using cdod.Models;
using cdod.Services;
using cdod.Services.DataLoaders;
using Microsoft.EntityFrameworkCore;
using Group = System.Text.RegularExpressions.Group;

namespace cdod.Schema.OutputTypes
{
    public class StudentType
    {

        [IsProjected]
        public int Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Patronymic { get; set; }

        public string? Description { get; set; }

        public DateOnly? BirthDate { get; set; }

        [IsProjected]
        public int? SchoolId { get; set; }

        [UseProjection]
        public async Task<SchoolType?> School([Service] SchoolDataLoader schoolDataLoader)
        {
            if(SchoolId is int schoolId)
            {
                var school = await schoolDataLoader.LoadAsync(schoolId);
                return new SchoolType()
                {
                    Id = school.Id,
                    Name = school.Name,
                    District = school.District
                };
            }

            return null;
        }

        [IsProjected]
        public int ParentId { get; set; }

        public async Task<ParentType> Parent([Service] ParentDataLoader parentDataLoader,
            [Service]UserDataLoader userDataLoader)
        {
            var parent = await parentDataLoader.LoadAsync(ParentId);
            return new ParentType(await userDataLoader.LoadAsync(ParentId))
            {
                Id = parent.UserId,
                SecondPhoneNumber = parent.SecondPhoneNumber,
                SecondEmail = parent.SecondEmail,
                SignDate = parent.SignDate,
                Type = parent.Type
            };
        }

        [UseProjection]
        public async Task<IEnumerable<InfoType>> Info([Service] StcDataLoader stcDataLoader)
        {
            IEnumerable<StudentToCourse> STCs = await stcDataLoader.LoadAsync(Id);

            return  STCs
                .Select(stc => new InfoType()
                {
                    CourseId = stc.CourseId,
                    StudentId = Id,
                    AdmissionDate = stc.SignDate,
                    IsGetRobot = stc.EquipmentPriceWithRobot,
                    ContractState = stc.ContractState
                });
        }
    }
}
