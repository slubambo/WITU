using System;
using WITU.Core.Model;
using FluentNHibernate.Mapping;

namespace WITU.Core.Mappings
{
	public class InformationDeskMapping : ClassMap<InformationDesk>
	{
		public InformationDeskMapping()
		{
			Table("`information_desk`");
			Schema("witu");
			Id(x => x.Id, "id")
				.GeneratedBy.Native();
			OptimisticLock.Version();
			Map(x => x.Content, "content");
			Map(x => x.CreatedOn, "createdOn");
			Map(x => x.DateOfEvent, "dateOfEvent");
			Map(x => x.DefaultImageFileName, "defaultImageFileName");
			Map(x => x.ShortDescription, "shortDescription");
			Map(x => x.ShowEndDate, "showEndDate");
			Map(x => x.ShowStartDate, "showStartDate");
			Map(x => x.Title, "title");
			Map(x => x.Type, "type");
			HasMany(x => x.InformationDeskAttachments)
				.KeyColumn("InformationDeskId")
				.Inverse()
				.Cascade.All()
				.Fetch.Select()
				.AsSet();
			HasMany(x => x.InformationDeskTargets)
				.KeyColumn("informationDeskId")
				.Inverse()
				.Cascade.All()
				.Fetch.Select()
				.AsSet();
			References(x => x.User)
				.Class(typeof(User))	
				.Column("userId")
				.Fetch.Select()
				.Cascade.All();
		    Cache.ReadWrite();
		}
	}
}