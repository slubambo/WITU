using System;
using WITU.Core.Model;
using FluentNHibernate.Mapping;

namespace WITU.Core.Mappings
{
	public class TemplateDocumentMapping : ClassMap<TemplateDocument>
	{
		public TemplateDocumentMapping()
		{
			Table("`template_document`");
			Schema("witu");
			Id(x => x.Id, "id")
				.GeneratedBy.Native();
			OptimisticLock.Version();
			Map(x => x.Category, "category");
			Map(x => x.Content, "content");
			Map(x => x.FileType, "fileType");
			Map(x => x.Name, "name");
			Map(x => x.Subject, "subject");
			
		}
	}
}