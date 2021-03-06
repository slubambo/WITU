using System;
using System.Collections.Generic;

namespace WITU.Core.Model
{
    [Serializable]
    public class AccessRole
    {
        public AccessRole()
        {
            UserRoles = new Iesi.Collections.Generic.HashedSet<UserRole>();
        }
        public virtual int Id
        {
            get;
            set;
        }
        public virtual int Level
        {
            get;
            set;
        }
        public virtual int LevelId
        {
            get;
            set;
        }
        public virtual string LevelName
        {
            get;
            set;
        }
        
        public virtual Iesi.Collections.Generic.ISet<UserRole> UserRoles { get; set; }
        public virtual User User
        {
            get;
            set;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            return Equals(obj as AccessRole);
        }

        public virtual bool Equals(AccessRole obj)
        {
            if (obj == null) return false;

            if (Equals(Id, obj.Id) == false) return false;
            if (Equals(Level, obj.Level) == false) return false;
            if (Equals(LevelId, obj.LevelId) == false) return false;
            if (Equals(LevelName, obj.LevelName) == false) return false;
            return true;
        }

        public override int GetHashCode()
        {
            int result = 1;

            result = (result * 397) ^ Id.GetHashCode();
            result = (result * 397) ^ Level.GetHashCode();
            result = (result * 397) ^ LevelId.GetHashCode();
            result = (result * 397) ^ (LevelName != null ? LevelName.GetHashCode() : 0);
            return result;
        }
    }
}