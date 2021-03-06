using System;
using System.Collections.Generic;

namespace WITU.Core.Model
{
	[Serializable]
	public class ProspectiveStudent
	{
		public ProspectiveStudent()
		{
			References = new Iesi.Collections.Generic.HashedSet<Reference>();
			Students = new Iesi.Collections.Generic.HashedSet<Student>();		
		}
		public virtual string ApplicationNumber
		{
			get;
			set;
		}
		public virtual DateTime CreatedOn
		{
			get;
			set;
		}
		public virtual int Id
		{
			get;
			set;
		}
		public virtual string SignitureName
		{
			get;
			set;
		}
		public virtual int? UnderGradEntryKind
		{
			get;
			set;
		}
	
		public virtual Person Person
		{
			get;
			set;
		}
	
		public virtual Iesi.Collections.Generic.ISet<Reference> References
		{
			get;
			set;
		}
		
		public virtual Iesi.Collections.Generic.ISet<Student> Students
		{
			get;
			set;
		}
		public virtual User User
		{
			get;
			set;
		}
		
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj))
				return true;
				
			return Equals(obj as ProspectiveStudent);
		}
		
		public virtual bool Equals(ProspectiveStudent obj)
		{
			if (obj == null) return false;

			if (Equals(ApplicationNumber, obj.ApplicationNumber) == false) return false;
			if (Equals(CreatedOn, obj.CreatedOn) == false) return false;
			if (Equals(Id, obj.Id) == false) return false;
			if (Equals(SignitureName, obj.SignitureName) == false) return false;
			if (Equals(UnderGradEntryKind, obj.UnderGradEntryKind) == false) return false;
			return true;
		}
		
		public override int GetHashCode()
		{
			int result = 1;

			result = (result * 397) ^ (ApplicationNumber != null ? ApplicationNumber.GetHashCode() : 0);
			result = (result * 397) ^ CreatedOn.GetHashCode();
			result = (result * 397) ^ Id.GetHashCode();
			result = (result * 397) ^ (SignitureName != null ? SignitureName.GetHashCode() : 0);
			result = (result * 397) ^ (UnderGradEntryKind != null ? UnderGradEntryKind.GetHashCode() : 0);
			return result;
		}
	}
}