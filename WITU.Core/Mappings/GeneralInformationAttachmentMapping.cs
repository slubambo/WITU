using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WITU.Core.Model;
using FluentNHibernate.Mapping;

namespace WITU.Core.Mappings
{
    public class GeneralInformationAttachmentMapping : ClassMap<GeneralInformationAttachment>
    {
        public GeneralInformationAttachmentMapping()
        {
            Table("`general_information_attachment`");
            Schema("witu");
            Id(x => x.Id, "id")
                .GeneratedBy.Native();
            OptimisticLock.Version();
            Map(x => x.UserFriendlyName, "userFriendlyName");
            Map(x => x.OriginalFileName, "originalFileName");
            Map(x => x.FileName, "fileName");
            Map(x => x.FileType, "fileType");
            Map(x => x.Description, "description");
            References(x => x.GeneralInformation)
                .Class<GeneralInformation>()
                .Column("generalInformationId")
                .Not.Nullable()
                .Fetch.Select()
                .Cascade.SaveUpdate();
            References(x => x.CohortYear)
                .Class<CohortYear>()
                .Column("academicYearId")
                .Nullable()
                .Fetch.Select()
                .Cascade.All();
            Cache.ReadWrite();
        }
    }
}
