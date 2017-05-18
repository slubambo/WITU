using System;
using WITU.Core.Model;
using FluentNHibernate.Mapping;

namespace WITU.Core.Mappings
{
	public class SubjectMapping : ClassMap<Subject>
	{
		public SubjectMapping()
		{
			Table("`subject`");
			Schema("witu");
			Id(x => x.Id, "id")
				.GeneratedBy.Native();
			OptimisticLock.Version();
			Map(x => x.Name, "name");
			
		    Cache.ReadWrite();
		}
	}
}