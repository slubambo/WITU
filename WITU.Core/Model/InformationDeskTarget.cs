using System;
using System.Collections.Generic;

namespace WITU.Core.Model
{
	[Serializable]
	public class InformationDeskTarget
	{
		public InformationDeskTarget()
		{		
		}
		public virtual int AccessLevel
		{
			get;
			set;
		}
		public virtual int AccessLevelId
		{
			get;
			set;
		}
		public virtual int Id
		{
			get;
			set;
		}
		public virtual int TargetType
		{
			get;
			set;
		}
		public virtual InformationDesk InformationDesk
		{
			get;
			set;
		}
		
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj))
				return true;
				
			return Equals(obj as InformationDeskTarget);
		}
		
		public virtual bool Equals(InformationDeskTarget obj)
		{
			if (obj == null) return false;

			if (Equals(AccessLevel, obj.AccessLevel) == false) return false;
			if (Equals(AccessLevelId, obj.AccessLevelId) == false) return false;
			if (Equals(Id, obj.Id) == false) return false;
			if (Equals(TargetType, obj.TargetType) == false) return false;
			return true;
		}
		
		public override int GetHashCode()
		{
			int result = 1;

			result = (result * 397) ^ AccessLevel.GetHashCode();
			result = (result * 397) ^ AccessLevelId.GetHashCode();
			result = (result * 397) ^ Id.GetHashCode();
			result = (result * 397) ^ TargetType.GetHashCode();
			return result;
		}
	}
}