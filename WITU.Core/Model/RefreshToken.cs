using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WITU.Core.Model
{
    [Serializable]
    public class RefreshToken
    {
        public virtual string Id { get; set; }

        public virtual string Subject { get; set; }

        public virtual string ClientId { get; set; }
        public virtual DateTime IssuedUtc { get; set; }
        public virtual DateTime ExpiresUtc { get; set; }

        public virtual string ProtectedTicket { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            return Equals(obj as RefreshToken);
        }

        public virtual bool Equals(RefreshToken obj)
        {
            if (obj == null) return false;

            if (Equals(Subject, obj.Subject) == false) return false;
            if (Equals(ClientId, obj.ClientId) == false) return false;
            if (Equals(Id, obj.Id) == false) return false;
            if (Equals(ProtectedTicket, obj.ProtectedTicket) == false) return false;
            if (Equals(IssuedUtc, obj.IssuedUtc) == false) return false;
            if (Equals(ExpiresUtc, obj.ExpiresUtc) == false) return false;
            return true;
        }

        public override int GetHashCode()
        {
            int result = 1;

            result = (result * 397) ^ (Id != null ? Id.GetHashCode() : 0);
            result = (result * 397) ^ (Subject != null ? Subject.GetHashCode() : 0);
            result = (result * 397) ^ (ClientId != null ? ClientId.GetHashCode() : 0);
            result = (result * 397) ^ (ProtectedTicket != null ? ProtectedTicket.GetHashCode() : 0);
            return result;
        }
    }
}
