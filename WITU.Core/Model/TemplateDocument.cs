using System;
using System.Collections.Generic;

namespace WITU.Core.Model
{
	[Serializable]
	public class TemplateDocument
	{
		public TemplateDocument()
		{
		}
		public virtual int Category
		{
			get;
			set;
		}
		public virtual string Content
		{
			get;
			set;
		}
		public virtual string FileType
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
		public virtual string Subject
		{
			get;
			set;
		}
		
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj))
				return true;
				
			return Equals(obj as TemplateDocument);
		}
		
		public virtual bool Equals(TemplateDocument obj)
		{
			if (obj == null) return false;

			if (Equals(Category, obj.Category) == false) return false;
			if (Equals(Content, obj.Content) == false) return false;
			if (Equals(FileType, obj.FileType) == false) return false;
			if (Equals(Id, obj.Id) == false) return false;
			if (Equals(Name, obj.Name) == false) return false;
			if (Equals(Subject, obj.Subject) == false) return false;
			return true;
		}
		
		public override int GetHashCode()
		{
			int result = 1;

			result = (result * 397) ^ Category.GetHashCode();
			result = (result * 397) ^ (Content != null ? Content.GetHashCode() : 0);
			result = (result * 397) ^ (FileType != null ? FileType.GetHashCode() : 0);
			result = (result * 397) ^ Id.GetHashCode();
			result = (result * 397) ^ (Name != null ? Name.GetHashCode() : 0);
			result = (result * 397) ^ (Subject != null ? Subject.GetHashCode() : 0);
			return result;
		}
	}
}