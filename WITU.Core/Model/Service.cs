using System;
using System.Collections.Generic;

namespace WITU.Core.Model
{
    [Serializable]
	public class Service
	{
		public Service()
		{
			Roles = new Iesi.Collections.Generic.HashedSet<Role>();		
		}
		public virtual int? Category
		{
			get;
			set;
		}
		public virtual int Id
		{
			get;
			set;
		}
		public virtual string Name
		{
			get;
			set;
		}
		public virtual Iesi.Collections.Generic.ISet<Role> Roles
		{
			get;
			set;
		}
		
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj))
				return true;
				
			return Equals(obj as Service);
		}
		
		public virtual bool Equals(Service obj)
		{
			if (obj == null) return false;

			if (Equals(Category, obj.Category) == false) return false;
			if (Equals(Id, obj.Id) == false) return false;
			if (Equals(Name, obj.Name) == false) return false;
			return true;
		}
		
		public override int GetHashCode()
		{
			int result = 1;

			result = (result * 397) ^ (Category != null ? Category.GetHashCode() : 0);
			result = (result * 397) ^ Id.GetHashCode();
			result = (result * 397) ^ (Name != null ? Name.GetHashCode() : 0);
			return result;
		}
	}
}