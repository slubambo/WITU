using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WITU.Core.Model;

namespace WITU.Core.Mappings
{
    public class SyllabusMapping : ClassMap<Syllabus>
    {
        public SyllabusMapping()
        {
            Table("`syllabus`");
            Schema("witu");
            Id(x => x.Id, "id")
                .GeneratedBy.Native();
            OptimisticLock.Version();
            Map(x => x.Title, "title");
            Map(x => x.Week, "week");
            Map(x => x.Overview, "overview");
            Map(x => x.Graded, "graded");
            References(x => x.Course)
                .Class(typeof(Course))
                .Not.Nullable()
                .Column("courseId")
                .Fetch.Select()
                .Cascade.All();
            HasMany(x => x.SyllabusAttachments)
                .KeyColumn("syllabusId")
                .Inverse()
                .Cascade.All()
                .Fetch.Select()
                .AsSet();
            Cache.ReadWrite();
        }
    }
}
