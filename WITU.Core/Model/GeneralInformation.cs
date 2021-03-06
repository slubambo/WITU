using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using WITU.Core.Validations;

namespace WITU.Core.Model
{
    [FluentValidation.Attributes.Validator(typeof(GeneralInformationValidator))]
	[Serializable]
	public class GeneralInformation
	{
		public GeneralInformation()
		{
		    GeneralInformationAttachments = new Iesi.Collections.Generic.HashedSet<GeneralInformationAttachment>();
		}
		[AllowHtml]
        public virtual string Content
		{
			get;
			set;
		}
		public virtual DateTime CreatedOn
		{
			get;
			set;
		}
		public virtual string DefaultImageFilelName
		{
			get;
			set;
		}
		public virtual int Id
		{
			get;
			set;
		}
		[Display(Name = "Short Description")]
        public virtual string ShortDescription
		{
			get;
			set;
		}
		public virtual string Title
		{
			get;
			set;
		}
		public virtual User User
		{
			get;
			set;
		}
        [Display(Name = "Upload Photo Attachment")]
        public virtual HttpPostedFileBase UploadImage { get; set; }

        public virtual Iesi.Collections.Generic.ISet<GeneralInformationAttachment> GeneralInformationAttachments 
        {
            get; set;
        } 

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj))
				return true;
				
			return Equals(obj as GeneralInformation);
		}
		
		public virtual bool Equals(GeneralInformation obj)
		{
			if (obj == null) return false;

			if (Equals(Content, obj.Content) == false) return false;
			if (Equals(CreatedOn, obj.CreatedOn) == false) return false;
			if (Equals(DefaultImageFilelName, obj.DefaultImageFilelName) == false) return false;
			if (Equals(Id, obj.Id) == false) return false;
			if (Equals(ShortDescription, obj.ShortDescription) == false) return false;
			if (Equals(Title, obj.Title) == false) return false;
			return true;
		}
		
		public override int GetHashCode()
		{
			int result = 1;

			result = (result * 397) ^ (Content != null ? Content.GetHashCode() : 0);
			result = (result * 397) ^ CreatedOn.GetHashCode();
			result = (result * 397) ^ (DefaultImageFilelName != null ? DefaultImageFilelName.GetHashCode() : 0);
			result = (result * 397) ^ Id.GetHashCode();
			result = (result * 397) ^ (ShortDescription != null ? ShortDescription.GetHashCode() : 0);
			result = (result * 397) ^ (Title != null ? Title.GetHashCode() : 0);
			return result;
		}
	}
}