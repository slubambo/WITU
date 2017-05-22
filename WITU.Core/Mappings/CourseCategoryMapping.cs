using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using WITU.Core.Model;

namespace WITU.Core.Mappings
{
    public class CourseCategoryMapping : ClassMap<CourseCategory>
    {
        public CourseCategoryMapping()
        {
            Table("`course_category`");
            Schema("witu");
            Id(x => x.Id, "id")
                .GeneratedBy.Native();
            OptimisticLock.Version();
            Map(x => x.Name, "name");
            Map(x => x.DescriptiveTitle, "descriptiveTitle");
            Map(x => x.Description, "description");
            Map(x => x.Creators, "creators");
            HasMany(x => x.Courses)
                .KeyColumn("courseCategoryId")
                .KeyNullable()
                .Inverse()
                .Cascade.All()
                .Fetch.Select()
                .AsSet();
            Cache.ReadWrite();
        }
    }
}
