using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using WITU.Core.Validations;

namespace WITU.Core.Model
{
	[Serializable]
    [FluentValidation.Attributes.Validator(typeof(UniversityValidator))]
	public class Institution
	{
		public Institution()
		{
			Campus = new Iesi.Collections.Generic.HashedSet<Campus>();		
		}
		public virtual DateTime? CreatedOn
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
        [Display(Name = "University Logo Path")]
		public virtual string LogoPathName
		{
			get;
			set;
		}
		public virtual string Name
		{
			get;
			set;
		}
		public virtual string ShortName
		{
			get;
			set;
		}
		public virtual Iesi.Collections.Generic.ISet<Campus> Campus
		{
			get;
			set;
		}
		
        public virtual HttpPostedFileBase logoFile { get; set; }

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj))
				return true;
				
			return Equals(obj as Institution);
		}
		
		public virtual bool Equals(Institution obj)
		{
			if (obj == null) return false;

			if (Equals(CreatedOn, obj.CreatedOn) == false) return false;
			if (Equals(Description, obj.Description) == false) return false;
			if (Equals(Id, obj.Id) == false) return false;
			if (Equals(LogoPathName, obj.LogoPathName) == false) return false;
			if (Equals(Name, obj.Name) == false) return false;
			if (Equals(ShortName, obj.ShortName) == false) return false;
			return true;
		}
		
		public override int GetHashCode()
		{
			int result = 1;

			result = (result * 397) ^ (CreatedOn != null ? CreatedOn.GetHashCode() : 0);
			result = (result * 397) ^ (Description != null ? Description.GetHashCode() : 0);
			result = (result * 397) ^ Id.GetHashCode();
			result = (result * 397) ^ (LogoPathName != null ? LogoPathName.GetHashCode() : 0);
			result = (result * 397) ^ (Name != null ? Name.GetHashCode() : 0);
			result = (result * 397) ^ (ShortName != null ? ShortName.GetHashCode() : 0);
			return result;
		}
	}
}