using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WITU.Core.Validations;

namespace WITU.Core.Model
{
	[Serializable]
    [FluentValidation.Attributes.Validator(typeof(CampusValidator))]
	public class Campus
	{
		public Campus()
		{
			//Programs = new Iesi.Collections.Generic.HashedSet<Program>();	
		}
		public virtual string Code
		{
			get;
			set;
		}
		public virtual string Description
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
		
        [Display(Name = "Short Name")]
        public virtual string ShortName
		{
			get;
			set;
		}
		
		//public virtual Iesi.Collections.Generic.ISet<Program> Programs
		//{
		//	get;
		//	set;
		//}
		
		public virtual Institution Institution
		{
			get;
			set;
		}
		
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj))
				return true;
				
			return Equals(obj as Campus);
		}
		
		public virtual bool Equals(Campus obj)
		{
			if (obj == null) return false;

			if (Equals(Code, obj.Code) == false) return false;
			if (Equals(Description, obj.Description) == false) return false;
			if (Equals(Id, obj.Id) == false) return false;
			if (Equals(Name, obj.Name) == false) return false;
			if (Equals(ShortName, obj.ShortName) == false) return false;
			return true;
		}
		
		public override int GetHashCode()
		{
			int result = 1;

			result = (result * 397) ^ (Code != null ? Code.GetHashCode() : 0);
			result = (result * 397) ^ (Description != null ? Description.GetHashCode() : 0);
			result = (result * 397) ^ Id.GetHashCode();
			result = (result * 397) ^ (Name != null ? Name.GetHashCode() : 0);
			result = (result * 397) ^ (ShortName != null ? ShortName.GetHashCode() : 0);
			return result;
		}
	}
}