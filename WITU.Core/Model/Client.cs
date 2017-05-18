using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WITU.Core.Model
{
    [Serializable]
    public class Client
    {

        public virtual string Id { get; set; }

        public virtual string Secret { get; set; }

        public virtual string Name { get; set; }
        public virtual ApplicationTypes ApplicationType { get; set; }
        public virtual bool Active { get; set; }
        /**
        * The time it will take the client's refresh token to expire in minutes
        */
        public virtual int RefreshTokenLifeTime { get; set; }
        /**
        * used configure CORS and to set “Access-Control-Allow-Origin” on the back-end API
        */
        public virtual string AllowedOrigin { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            return Equals(obj as Client);
        }

        public virtual bool Equals(Client obj)
        {
            if (obj == null) return false;

            if (Equals(Secret, obj.Secret) == false) return false;
            if (Equals(ApplicationType, obj.ApplicationType) == false) return false;
            if (Equals(Id, obj.Id) == false) return false;
            if (Equals(Name, obj.Name) == false) return false;
            if (Active != obj.Active) return false;
            if (Equals(RefreshTokenLifeTime, obj.RefreshTokenLifeTime) == false) return false;
            if (Equals(AllowedOrigin, obj.AllowedOrigin) == false) return false;
            return true;
        }

        public override int GetHashCode()
        {
            int result = 1;

            result = (result * 397) ^ (Id != null ? Id.GetHashCode() : 0);
            result = (result * 397) ^ (Secret != null ? Secret.GetHashCode() : 0);
            result = (result * 397) ^ (AllowedOrigin != null ? AllowedOrigin.GetHashCode() : 0);
            result = (result * 397) ^ (Name != null ? Name.GetHashCode() : 0);
            return result;
        }
    }

    public enum ApplicationTypes
    {
        JavaScript = 0,
        NativeConfidential = 1
    }

}
