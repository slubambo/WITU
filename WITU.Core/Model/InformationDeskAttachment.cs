using System;
using System.Collections.Generic;

namespace WITU.Core.Model
{
	[Serializable]
	public class InformationDeskAttachment
	{
		public InformationDeskAttachment()
		{		
		}
		public virtual string FileNameSaved
		{
			get;
			set;
		}
		public virtual int Id
		{
			get;
			set;
		}
		public virtual string OriginalFileName
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
				
			return Equals(obj as InformationDeskAttachment);
		}
		
		public virtual bool Equals(InformationDeskAttachment obj)
		{
			if (obj == null) return false;

			if (Equals(FileNameSaved, obj.FileNameSaved) == false) return false;
			if (Equals(Id, obj.Id) == false) return false;
			if (Equals(OriginalFileName, obj.OriginalFileName) == false) return false;
			return true;
		}
		
		public override int GetHashCode()
		{
			int result = 1;

			result = (result * 397) ^ (FileNameSaved != null ? FileNameSaved.GetHashCode() : 0);
			result = (result * 397) ^ Id.GetHashCode();
			result = (result * 397) ^ (OriginalFileName != null ? OriginalFileName.GetHashCode() : 0);
			return result;
		}
	}
}