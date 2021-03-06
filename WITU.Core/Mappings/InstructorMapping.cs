using WITU.Core.Model;
using FluentNHibernate.Mapping;

namespace WITU.Core.Mappings
{
    public class InstructorMapping : ClassMap<Instructor>
    {
        public InstructorMapping()
        {
            Table("`instructor`");
            Schema("witu");
            Id(x => x.Id, "id")
                .GeneratedBy.Native();
            OptimisticLock.Version();
            Map(x => x.About, "about");
            Map(x => x.CreatedOn, "createdOn");
            Map(x => x.ResearchInterests, "researchInterests");
            //Map(x => x.UserId, "userId");
            References(x => x.Person)
                .Class(typeof (Person))
                .Not.Nullable()
                .Column("personId")
                .Fetch.Select()
                .Cascade.All();
            //HasMany(x => x.StaffCourses)
            //    .KeyColumn("staffId")
            //    .Inverse()
            //    .Cascade.All()
            //    .Fetch.Select()
            //    .AsSet();
            HasMany(x => x.InstructorPositions)
                .KeyColumn("instructorId")
                .Inverse()
                .Cascade.All()
                .Fetch.Select()
                .AsSet();
            References(x => x.User)
                .Class(typeof (User))
                .Column("userId")
                .Fetch.Select()
                .Cascade.All();
            Cache.ReadWrite();
        }
    }
}