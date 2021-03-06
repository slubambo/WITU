using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using WITU.Core.Validations;

namespace WITU.Core.Model
{
    [FluentValidation.Attributes.Validator(typeof(InformationDeskValidator))]
	[Serializable]
	public class InformationDesk
	{
		public InformationDesk()
		{
			InformationDeskAttachments = new Iesi.Collections.Generic.HashedSet<InformationDeskAttachment>();
			InformationDeskTargets = new Iesi.Collections.Generic.HashedSet<InformationDeskTarget>();
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
		public virtual DateTime? DateOfEvent
		{
			get;
			set;
		}
		public virtual string DefaultImageFileName
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
		public virtual DateTime? ShowEndDate
		{
			get;
			set;
		}
		public virtual DateTime? ShowStartDate
		{
			get;
			set;
		}
		public virtual string Title
		{
			get;
			set;
		}
		public virtual int Type
		{
			get;
			set;
		}
        [Display(Name = "Upload Attached Image")]
        public virtual HttpPostedFileBase UploadImage { get; set; }
		public virtual Iesi.Collections.Generic.ISet<InformationDeskAttachment> InformationDeskAttachments
		{
			get;
			set;
		}
		public virtual Iesi.Collections.Generic.ISet<InformationDeskTarget> InformationDeskTargets
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
				
			return Equals(obj as InformationDesk);
		}
		
		public virtual bool Equals(InformationDesk obj)
		{
			if (obj == null) return false;

			if (Equals(Content, obj.Content) == false) return false;
			if (Equals(CreatedOn, obj.CreatedOn) == false) return false;
			if (Equals(DateOfEvent, obj.DateOfEvent) == false) return false;
			if (Equals(DefaultImageFileName, obj.DefaultImageFileName) == false) return false;
			if (Equals(Id, obj.Id) == false) return false;
			if (Equals(ShortDescription, obj.ShortDescription) == false) return false;
			if (Equals(ShowEndDate, obj.ShowEndDate) == false) return false;
			if (Equals(ShowStartDate, obj.ShowStartDate) == false) return false;
			if (Equals(Title, obj.Title) == false) return false;
			if (Equals(Type, obj.Type) == false) return false;
			return true;
		}
		
		public override int GetHashCode()
		{
			int result = 1;

			result = (result * 397) ^ (Content != null ? Content.GetHashCode() : 0);
			result = (result * 397) ^ CreatedOn.GetHashCode();
			result = (result * 397) ^ (DateOfEvent != null ? DateOfEvent.GetHashCode() : 0);
			result = (result * 397) ^ (DefaultImageFileName != null ? DefaultImageFileName.GetHashCode() : 0);
			result = (result * 397) ^ Id.GetHashCode();
			result = (result * 397) ^ (ShortDescription != null ? ShortDescription.GetHashCode() : 0);
			result = (result * 397) ^ (ShowEndDate != null ? ShowEndDate.GetHashCode() : 0);
			result = (result * 397) ^ (ShowStartDate != null ? ShowStartDate.GetHashCode() : 0);
			result = (result * 397) ^ (Title != null ? Title.GetHashCode() : 0);
			result = (result * 397) ^ Type.GetHashCode();
			return result;
		}
	}
}