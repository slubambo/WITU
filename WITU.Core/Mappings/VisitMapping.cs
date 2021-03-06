using System;
using WITU.Core.Model;
using FluentNHibernate.Mapping;

namespace WITU.Core.Mappings
{
	public class VisitMapping : ClassMap<Visit>
	{
		public VisitMapping()
		{
			Table("`visit`");
			Schema("witu");
			Id(x => x.Id, "id")
				.GeneratedBy.Native();
			OptimisticLock.Version();
			Map(x => x.Browser, "browser");
			Map(x => x.BrowserVersion, "browserVersion");
			Map(x => x.LoginDate, "loginDate");
			Map(x => x.Username, "username");
			References(x => x.User)
				.Class(typeof(User))	
				.Column("userId")
				.Fetch.Select()
				.Cascade.All();
		}
	}
}