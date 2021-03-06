using System;
using System.Collections.Generic;

namespace WITU.Core.Model
{
	[Serializable]
	public class Log
	{
		public Log()
		{		
		}
		public virtual int Category
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
		public virtual string Information
		{
			get;
			set;
		}
		public virtual string UserDescription
		{
			get;
			set;
		}
		public virtual string Username
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
				
			return Equals(obj as Log);
		}
		
		public virtual bool Equals(Log obj)
		{
			if (obj == null) return false;

			if (Equals(Category, obj.Category) == false) return false;
			if (Equals(CreatedOn, obj.CreatedOn) == false) return false;
			if (Equals(Id, obj.Id) == false) return false;
			if (Equals(Information, obj.Information) == false) return false;
			if (Equals(UserDescription, obj.UserDescription) == false) return false;
			if (Equals(Username, obj.Username) == false) return false;
			return true;
		}
		
		public override int GetHashCode()
		{
			int result = 1;

			result = (result * 397) ^ Category.GetHashCode();
			result = (result * 397) ^ CreatedOn.GetHashCode();
			result = (result * 397) ^ Id.GetHashCode();
			result = (result * 397) ^ (Information != null ? Information.GetHashCode() : 0);
			result = (result * 397) ^ (UserDescription != null ? UserDescription.GetHashCode() : 0);
			result = (result * 397) ^ (Username != null ? Username.GetHashCode() : 0);
			return result;
		}
	}
}