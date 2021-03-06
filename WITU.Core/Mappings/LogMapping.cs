using System;
using WITU.Core.Model;
using FluentNHibernate.Mapping;

namespace WITU.Core.Mappings
{
	public class LogMapping : ClassMap<Log>
	{
		public LogMapping()
		{
			Table("`log`");
			Schema("witu");
			Id(x => x.Id, "id")
				.GeneratedBy.Native();
			OptimisticLock.Version();
			Map(x => x.Category, "category");
			Map(x => x.CreatedOn, "createdOn");
			Map(x => x.Information, "information");
			Map(x => x.UserDescription, "userDescription");
			Map(x => x.Username, "username");
			References(x => x.User)
				.Class(typeof(User))	
				.Column("userId")
				.Fetch.Select()
				.Cascade.All();
		}
	}
}