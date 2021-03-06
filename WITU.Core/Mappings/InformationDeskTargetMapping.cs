using System;
using WITU.Core.Model;
using FluentNHibernate.Mapping;

namespace WITU.Core.Mappings
{
	public class InformationDeskTargetMapping : ClassMap<InformationDeskTarget>
	{
		public InformationDeskTargetMapping()
		{
			Table("`information_desk_target`");
			Schema("witu");
			Id(x => x.Id, "id")
				.GeneratedBy.Native();
			OptimisticLock.Version();
			Map(x => x.AccessLevel, "accessLevel");
			Map(x => x.AccessLevelId, "accessLevelId");
			Map(x => x.TargetType, "targetType");
			References(x => x.InformationDesk)
				.Class(typeof(InformationDesk))
				.Not.Nullable()	
				.Column("informationDeskId")
				.Fetch.Select()
				.Cascade.All();
		    Cache.ReadWrite();
		}
	}
}