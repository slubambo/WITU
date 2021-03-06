using System;
using System.Collections.Generic;

namespace WITU.Core.Model
{
	[Serializable]
	public class SubjectWeightingCriterium
	{
		public SubjectWeightingCriterium()
		{		
		}
		public virtual int Id
		{
			get;
			set;
		}
		public virtual int SubjectRelation
		{
			get;
			set;
		}
		
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj))
				return true;
				
			return Equals(obj as SubjectWeightingCriterium);
		}
		
		public virtual bool Equals(SubjectWeightingCriterium obj)
		{
			if (obj == null) return false;

			if (Equals(Id, obj.Id) == false) return false;
			if (Equals(SubjectRelation, obj.SubjectRelation) == false) return false;
			return true;
		}
		
		public override int GetHashCode()
		{
			int result = 1;

			result = (result * 397) ^ Id.GetHashCode();
			result = (result * 397) ^ SubjectRelation.GetHashCode();
			return result;
		}
	}
}