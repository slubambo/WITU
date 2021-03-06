using System;
using System.Collections.Generic;

namespace WITU.Core.Model
{
	[Serializable]
	public class Reference
	{
		public Reference()
		{		
		}
		public virtual string AttachedFileName
		{
			get;
			set;
		}
		public virtual string Desigination
		{
			get;
			set;
		}
		public virtual int Id
		{
			get;
			set;
		}
		public virtual string Organization
		{
			get;
			set;
		}
		public virtual string Relationship
		{
			get;
			set;
		}
		public virtual Person Person
		{
			get;
			set;
		}
		public virtual ProspectiveStudent ProspectiveStudent
		{
			get;
			set;
		}
		
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj))
				return true;
				
			return Equals(obj as Reference);
		}
		
		public virtual bool Equals(Reference obj)
		{
			if (obj == null) return false;

			if (Equals(AttachedFileName, obj.AttachedFileName) == false) return false;
			if (Equals(Desigination, obj.Desigination) == false) return false;
			if (Equals(Id, obj.Id) == false) return false;
			if (Equals(Organization, obj.Organization) == false) return false;
			if (Equals(Relationship, obj.Relationship) == false) return false;
			return true;
		}
		
		public override int GetHashCode()
		{
			int result = 1;

			result = (result * 397) ^ (AttachedFileName != null ? AttachedFileName.GetHashCode() : 0);
			result = (result * 397) ^ (Desigination != null ? Desigination.GetHashCode() : 0);
			result = (result * 397) ^ Id.GetHashCode();
			result = (result * 397) ^ (Organization != null ? Organization.GetHashCode() : 0);
			result = (result * 397) ^ (Relationship != null ? Relationship.GetHashCode() : 0);
			return result;
		}
	}
}