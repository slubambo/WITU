using System;
using System.Collections.Generic;

namespace WITU.Core.Model
{
	[Serializable]
	public class County
	{
		public County()
		{
			People = new Iesi.Collections.Generic.HashedSet<Person>();
			People1 = new Iesi.Collections.Generic.HashedSet<Person>();
			SubCounties = new Iesi.Collections.Generic.HashedSet<SubCounty>();		
		}
		public virtual string Code
		{
			get;
			set;
		}
		public virtual DateTime? CreatedOn
		{
			get;
			set;
		}
		public virtual int Id
		{
			get;
			set;
		}
		public virtual DateTime? LastModified
		{
			get;
			set;
		}
		public virtual string Name
		{
			get;
			set;
		}
		public virtual District District
		{
			get;
			set;
		}
		public virtual Iesi.Collections.Generic.ISet<Person> People
		{
			get;
			set;
		}
		public virtual Iesi.Collections.Generic.ISet<Person> People1
		{
			get;
			set;
		}
		public virtual Iesi.Collections.Generic.ISet<SubCounty> SubCounties
		{
			get;
			set;
		}
		
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj))
				return true;
				
			return Equals(obj as County);
		}
		
		public virtual bool Equals(County obj)
		{
			if (obj == null) return false;

			if (Equals(Code, obj.Code) == false) return false;
			if (Equals(CreatedOn, obj.CreatedOn) == false) return false;
			if (Equals(Id, obj.Id) == false) return false;
			if (Equals(LastModified, obj.LastModified) == false) return false;
			if (Equals(Name, obj.Name) == false) return false;
			return true;
		}
		
		public override int GetHashCode()
		{
			int result = 1;

			result = (result * 397) ^ (Code != null ? Code.GetHashCode() : 0);
			result = (result * 397) ^ (CreatedOn != null ? CreatedOn.GetHashCode() : 0);
			result = (result * 397) ^ Id.GetHashCode();
			result = (result * 397) ^ (LastModified != null ? LastModified.GetHashCode() : 0);
			result = (result * 397) ^ (Name != null ? Name.GetHashCode() : 0);
			return result;
		}
	}
}