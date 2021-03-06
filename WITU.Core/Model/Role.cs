using System;
using System.Collections.Generic;

namespace WITU.Core.Model
{
    [Serializable]
    public class Role
    {
        public Role()
        {
            UserRoles = new Iesi.Collections.Generic.HashedSet<UserRole>();
        }
        public virtual int Id
        {
            get;
            set;
        }
        public virtual bool RequiresAccessScope
        {
            get;
            set;
        }

        public virtual bool CanCreate
        {
            get;
            set;
        }

        public virtual bool CanRead
        {
            get;
            set;
        }

        public virtual bool CanUpdate
        {
            get;
            set;
        }

        public virtual bool CanDelete
        {
            get;
            set;
        }

        public virtual int Type
        {
            get;
            set;
        }

        public virtual Iesi.Collections.Generic.ISet<UserRole> UserRoles { get; set; }
        public virtual Service Service
        {
            get;
            set;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            return Equals(obj as Role);
        }

        public virtual bool Equals(Role obj)
        {
            if (obj == null) return false;

            if (Equals(Id, obj.Id) == false) return false;
            if (Equals(RequiresAccessScope, obj.RequiresAccessScope) == false) return false;
            if (Equals(Type, obj.Type) == false) return false;
            return true;
        }

        public override int GetHashCode()
        {
            int result = 1;

            result = (result * 397) ^ Id.GetHashCode();
            result = (result * 397) ^ RequiresAccessScope.GetHashCode();
            result = (result * 397) ^ Type.GetHashCode();
            return result;
        }
    }
}