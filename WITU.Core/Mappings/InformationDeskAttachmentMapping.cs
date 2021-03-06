using System;
using WITU.Core.Model;
using FluentNHibernate.Mapping;

namespace WITU.Core.Mappings
{
	public class InformationDeskAttachmentMapping : ClassMap<InformationDeskAttachment>
	{
		public InformationDeskAttachmentMapping()
		{
			Table("`information_desk_attachment`");
			Schema("witu");
			Id(x => x.Id, "id")
				.GeneratedBy.Native();
			OptimisticLock.Version();
			Map(x => x.FileNameSaved, "fileNameSaved");
			Map(x => x.OriginalFileName, "originalFileName");
			References(x => x.InformationDesk)
				.Class(typeof(InformationDesk))
				.Not.Nullable()	
				.Column("InformationDeskId")
				.Fetch.Select()
				.Cascade.All();
		    Cache.ReadWrite();
		}
	}
}