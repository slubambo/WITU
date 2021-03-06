using System;
using WITU.Core.Model;
using FluentNHibernate.Mapping;

namespace WITU.Core.Mappings
{
	public class GeneralInformationMapping : ClassMap<GeneralInformation>
	{
		public GeneralInformationMapping()
		{
			Table("`general_information`");
			Schema("witu");
			Id(x => x.Id, "id")
				.GeneratedBy.Native();
			OptimisticLock.Version();
			Map(x => x.Content, "content");
			Map(x => x.CreatedOn, "createdOn");
			Map(x => x.DefaultImageFilelName, "defaultImageFilelName");
			Map(x => x.ShortDescription, "shortDescription");
			Map(x => x.Title, "title");
			References(x => x.User)
				.Class(typeof(User))	
				.Column("userId")
				.Fetch.Select()
				.Cascade.All();
            HasMany(x => x.GeneralInformationAttachments)
                .KeyColumn("generalInformationId")
                .Inverse()
                .Cascade.All()
                .Fetch.Select()
                .AsSet();
		    Cache.ReadWrite();
		}
	}
}