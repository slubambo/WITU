using System;
using System.Collections.Generic;

namespace WITU.Core.Model
{
	[Serializable]
	public class Country
	{
		public Country()
		{
			People = new Iesi.Collections.Generic.HashedSet<Person>();
			People1 = new Iesi.Collections.Generic.HashedSet<Person>();
			People2 = new Iesi.Collections.Generic.HashedSet<Person>();		
		}
		public virtual string CountryCode
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
		public virtual string Nationality
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
		public virtual Iesi.Collections.Generic.ISet<Person> People2
		{
			get;
			set;
		}
		
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj))
				return true;
				
			return Equals(obj as Country);
		}
		
		public virtual bool Equals(Country obj)
		{
			if (obj == null) return false;

			if (Equals(CountryCode, obj.CountryCode) == false) return false;
			if (Equals(CreatedOn, obj.CreatedOn) == false) return false;
			if (Equals(Id, obj.Id) == false) return false;
			if (Equals(LastModified, obj.LastModified) == false) return false;
			if (Equals(Name, obj.Name) == false) return false;
			if (Equals(Nationality, obj.Nationality) == false) return false;
			return true;
		}
		
		public override int GetHashCode()
		{
			int result = 1;

			result = (result * 397) ^ (CountryCode != null ? CountryCode.GetHashCode() : 0);
			result = (result * 397) ^ (CreatedOn != null ? CreatedOn.GetHashCode() : 0);
			result = (result * 397) ^ Id.GetHashCode();
			result = (result * 397) ^ (LastModified != null ? LastModified.GetHashCode() : 0);
			result = (result * 397) ^ (Name != null ? Name.GetHashCode() : 0);
			result = (result * 397) ^ (Nationality != null ? Nationality.GetHashCode() : 0);
			return result;
		}
	}
}