using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WITU.Core.Model
{
    [Serializable]
    public class Audit
    {
        public virtual int Id { get; set; }

        public virtual string IpAddress { get; set; }
        public virtual string UserName { get; set; }
        public virtual string UrlAccessed { get; set; }
        public virtual DateTime TimeAccessed { get; set; }
        //A new Data property that is going to store JSON string objects that will later be able to
        //be deserialized into objects if necessary to view details about a Request
        public virtual string Data { get; set; }

        public virtual string Message { get; set; }

        public virtual string UserType { get; set; }
    }
}
