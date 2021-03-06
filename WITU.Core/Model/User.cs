using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WITU.Core.Model
{
    public enum UserTypes
    {
        Instructor = 1,

        Student = 2,

        [Description("Prospective Student")]
        ProspectiveStudent = 3
    }

	[Serializable]
	public class User
	{
		public User()
		{
			AccessRoles = new Iesi.Collections.Generic.HashedSet<AccessRole>();
			GeneralInformations = new Iesi.Collections.Generic.HashedSet<GeneralInformation>();
			InformationDesks = new Iesi.Collections.Generic.HashedSet<InformationDesk>();
			Logs = new Iesi.Collections.Generic.HashedSet<Log>();
			ProspectiveStudents = new Iesi.Collections.Generic.HashedSet<ProspectiveStudent>();
			Staffs = new Iesi.Collections.Generic.HashedSet<Instructor>();
			Students = new Iesi.Collections.Generic.HashedSet<Student>();
			Visits = new Iesi.Collections.Generic.HashedSet<Visit>();
		    UserRoles = new Iesi.Collections.Generic.HashedSet<UserRole>();
		}
		public virtual string Comment
		{
			get;
			set;
		}
		public virtual string Email
		{
			get;
			set;
		}
		public virtual int? FailedPasswordAttemptsCount
		{
			get;
			set;
		}
		public virtual DateTime? FailedPasswordAttemptWindowStart
		{
			get;
			set;
		}
		public virtual int Id
		{
			get;
			set;
		}
		public virtual bool IsApproved
		{
			get;
			set;
		}
		public virtual bool IsFirstTimeUser
		{
			get;
			set;
		}
		public virtual bool IsLockedOut
		{
			get;
			set;
		}
		public virtual DateTime? LastActivityDate
		{
			get;
			set;
		}
		public virtual DateTime? LastLoginDate
		{
			get;
			set;
		}
		public virtual DateTime? LastPasswordChangeDate
		{
			get;
			set;
		}
		public virtual string Password
		{
			get;
			set;
		}
		public virtual string PasswordKey
		{
			get;
			set;
		}

        [Required]
		public virtual string Username
		{
			get;
			set;
		}
		public virtual int UserType
		{
			get;
			set;
		}

        public virtual string ProfilePhotoName
        {
            get;
            set;
        }

        public virtual string AltProfilePhotoName
        {
            get;
            set;
        }
		public virtual Iesi.Collections.Generic.ISet<AccessRole> AccessRoles
		{
			get;
			set;
		}
		
		public virtual Iesi.Collections.Generic.ISet<GeneralInformation> GeneralInformations
		{
			get;
			set;
		}
		public virtual Iesi.Collections.Generic.ISet<InformationDesk> InformationDesks
		{
			get;
			set;
		}
		public virtual Iesi.Collections.Generic.ISet<Log> Logs
		{
			get;
			set;
		}
		
		public virtual Iesi.Collections.Generic.ISet<ProspectiveStudent> ProspectiveStudents
		{
			get;
			set;
		}
		
		public virtual Iesi.Collections.Generic.ISet<Instructor> Staffs
		{
			get;
			set;
		}
		public virtual Iesi.Collections.Generic.ISet<Student> Students
		{
			get;
			set;
		}
		public virtual Iesi.Collections.Generic.ISet<Visit> Visits
		{
			get;
			set;
		}

        public virtual Iesi.Collections.Generic.ISet<UserRole> UserRoles { get; set; } 
		
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj))
				return true;
				
			return Equals(obj as User);
		}
		
		public virtual bool Equals(User obj)
		{
			if (obj == null) return false;

			if (Equals(Comment, obj.Comment) == false) return false;
			if (Equals(Email, obj.Email) == false) return false;
			if (Equals(FailedPasswordAttemptsCount, obj.FailedPasswordAttemptsCount) == false) return false;
			if (Equals(FailedPasswordAttemptWindowStart, obj.FailedPasswordAttemptWindowStart) == false) return false;
			if (Equals(Id, obj.Id) == false) return false;
			if (Equals(IsApproved, obj.IsApproved) == false) return false;
			if (Equals(IsFirstTimeUser, obj.IsFirstTimeUser) == false) return false;
			if (Equals(IsLockedOut, obj.IsLockedOut) == false) return false;
			if (Equals(LastActivityDate, obj.LastActivityDate) == false) return false;
			if (Equals(LastLoginDate, obj.LastLoginDate) == false) return false;
			if (Equals(LastPasswordChangeDate, obj.LastPasswordChangeDate) == false) return false;
			if (Equals(Password, obj.Password) == false) return false;
			if (Equals(PasswordKey, obj.PasswordKey) == false) return false;
			if (Equals(Username, obj.Username) == false) return false;
			if (Equals(UserType, obj.UserType) == false) return false;
			return true;
		}
		
		public override int GetHashCode()
		{
			int result = 1;

			result = (result * 397) ^ (Comment != null ? Comment.GetHashCode() : 0);
			result = (result * 397) ^ (Email != null ? Email.GetHashCode() : 0);
			result = (result * 397) ^ (FailedPasswordAttemptsCount != null ? FailedPasswordAttemptsCount.GetHashCode() : 0);
			result = (result * 397) ^ (FailedPasswordAttemptWindowStart != null ? FailedPasswordAttemptWindowStart.GetHashCode() : 0);
			result = (result * 397) ^ Id.GetHashCode();
			result = (result * 397) ^ IsApproved.GetHashCode();
			result = (result * 397) ^ IsFirstTimeUser.GetHashCode();
			result = (result * 397) ^ IsLockedOut.GetHashCode();
			result = (result * 397) ^ (LastActivityDate != null ? LastActivityDate.GetHashCode() : 0);
			result = (result * 397) ^ (LastLoginDate != null ? LastLoginDate.GetHashCode() : 0);
			result = (result * 397) ^ (LastPasswordChangeDate != null ? LastPasswordChangeDate.GetHashCode() : 0);
			result = (result * 397) ^ (Password != null ? Password.GetHashCode() : 0);
			result = (result * 397) ^ (PasswordKey != null ? PasswordKey.GetHashCode() : 0);
			result = (result * 397) ^ (Username != null ? Username.GetHashCode() : 0);
			result = (result * 397) ^ UserType.GetHashCode();
			return result;
		}
	}
}