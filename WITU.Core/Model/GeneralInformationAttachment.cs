using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WITU.Core.Model
{
    [Serializable]
    public class GeneralInformationAttachment
    {
        public virtual int Id { get; set; }

        public virtual string UserFriendlyName { get; set; }

        public virtual string OriginalFileName { get; set; }

        public virtual string FileName { get; set; }

        public virtual string FileType { get; set; }

        public virtual string Description { get; set; }

        public virtual GeneralInformation GeneralInformation { get; set; }

        public virtual CohortYear CohortYear { get; set; }

    }
}
