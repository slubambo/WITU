using System;
using System.Collections.Generic;

namespace WITU.Core.Model
{
	[Serializable]
	public class Visit
	{
		public Visit()
		{		
		}
		public virtual string Browser
		{
			get;
			set;
		}
		public virtual string BrowserVersion
		{
			get;
			set;
		}
		public virtual int Id
		{
			get;
			set;
		}
		public virtual DateTime LoginDate
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
				
			return Equals(obj as Visit);
		}
		
		public virtual bool Equals(Visit obj)
		{
			if (obj == null) return false;

			if (Equals(Browser, obj.Browser) == false) return false;
			if (Equals(BrowserVersion, obj.BrowserVersion) == false) return false;
			if (Equals(Id, obj.Id) == false) return false;
			if (Equals(LoginDate, obj.LoginDate) == false) return false;
			if (Equals(Username, obj.Username) == false) return false;
			return true;
		}
		
		public override int GetHashCode()
		{
			int result = 1;

			result = (result * 397) ^ (Browser != null ? Browser.GetHashCode() : 0);
			result = (result * 397) ^ (BrowserVersion != null ? BrowserVersion.GetHashCode() : 0);
			result = (result * 397) ^ Id.GetHashCode();
			result = (result * 397) ^ LoginDate.GetHashCode();
			result = (result * 397) ^ (Username != null ? Username.GetHashCode() : 0);
			return result;
		}
	}
}