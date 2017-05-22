using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WITU.Core.Model;

namespace WITU.Core.Mappings
{
    
    public class SyllabusAttachmentMapping : ClassMap<SyllabusAttachment>
    {
        public SyllabusAttachmentMapping()
        {
            Table("`syllabus_attachment`");
            Schema("witu");
            Id(x => x.Id, "id")
                .GeneratedBy.Native();
            OptimisticLock.Version();
            Map(x => x.UserFriendlyName, "userFriendlyName");
            Map(x => x.OriginalFileName, "originalFileName");
            Map(x => x.FileName, "fileName");
            Map(x => x.FileType, "fileType");
            Map(x => x.Description, "description");
            References(x => x.Syllabus)
                .Class<Syllabus>()
                .Column("syllabusId")
                .Not.Nullable()
                .Fetch.Select()
                .Cascade.SaveUpdate();
            Cache.ReadWrite();
        }
    }
}
