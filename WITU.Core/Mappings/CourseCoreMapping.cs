using System;
using WITU.Core.Model;
using FluentNHibernate.Mapping;

namespace WITU.Core.Mappings
{
	public class CourseCoreMapping : ClassMap<CourseCore>
	{
		public CourseCoreMapping()
		{
			Table("`course_core`");
			Schema("witu");
			Id(x => x.Id, "id")
				.GeneratedBy.Native();
			OptimisticLock.Version();
			Map(x => x.Overview, "overview");
            Map(x => x.Aims, "aims");
            Map(x => x.OutComes, "outcomes");
            Map(x => x.Skills, "skills");
            Map(x => x.Name, "name");
            Map(x => x.Code, "code");
			References(x => x.Subject)
				.Class(typeof(Subject))	
				.Column("subjectId")
				.Fetch.Select()
				.Cascade.All();
		}
	}
}