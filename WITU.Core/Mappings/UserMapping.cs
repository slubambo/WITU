using System;
using WITU.Core.Model;
using WITU.Core.Utils;
using FluentNHibernate.Mapping;

namespace WITU.Core.Mappings
{
	public class UserMapping : ClassMap<User>
	{
		public UserMapping()
		{
			Table("`user`");
			Schema("witu");
			Id(x => x.Id, "id")
				.GeneratedBy.Native();
			OptimisticLock.Version();
			Map(x => x.Comment, "comment");
			Map(x => x.Email, "email");
			Map(x => x.FailedPasswordAttemptsCount, "failedPasswordAttemptsCount");
			Map(x => x.FailedPasswordAttemptWindowStart, "failedPasswordAttemptWindowStart");
            Map(x => x.IsApproved, "isApproved").CustomType<BoolAsTinyInt>();
            Map(x => x.IsFirstTimeUser, "isFirstTimeUser").CustomType<BoolAsTinyInt>();
            Map(x => x.IsLockedOut, "isLockedOut").CustomType<BoolAsTinyInt>();
			Map(x => x.LastActivityDate, "lastActivityDate");
			Map(x => x.LastLoginDate, "lastLoginDate");
			Map(x => x.LastPasswordChangeDate, "lastPasswordChangeDate");
			Map(x => x.Password, "password");
			Map(x => x.PasswordKey, "passwordKey");
			Map(x => x.Username, "username");
			Map(x => x.UserType, "userType");
            Map(x => x.ProfilePhotoName, "profilePhotoName");
			HasMany(x => x.AccessRoles)
				.KeyColumn("userId")
				.Inverse()
				.Cascade.All()
				.Fetch.Select()
				.AsSet();
			
			HasMany(x => x.GeneralInformations)
				.KeyColumn("userId")
				.Inverse()
				.Cascade.All()
				.Fetch.Select()
				.AsSet();
			HasMany(x => x.InformationDesks)
				.KeyColumn("userId")
				.Inverse()
				.Cascade.All()
				.Fetch.Select()
				.AsSet();
			HasMany(x => x.Logs)
				.KeyColumn("userId")
				.Inverse()
				.Cascade.All()
				.Fetch.Select()
				.AsSet();
			
			HasMany(x => x.ProspectiveStudents)
				.KeyColumn("userId")
				.Inverse()
				.Cascade.All()
				.Fetch.Select()
				.AsSet();
			
			HasMany(x => x.Staffs)
				.KeyColumn("userId")
				.Inverse()
				.Cascade.All()
				.Fetch.Select()
				.AsSet();
			HasMany(x => x.Students)
				.KeyColumn("userId")
				.Inverse()
				.Cascade.All()
				.Fetch.Select()
				.AsSet();
			HasMany(x => x.Visits)
				.KeyColumn("userId")
				.Inverse()
				.Cascade.All()
				.Fetch.Select()
				.AsSet();
            HasMany(x => x.UserRoles)
                .KeyColumn("userId")
                .Inverse()
                .Cascade.All()
                .Fetch.Select()
                .AsSet();
		    //Cache.ReadWrite();
		}
	}
}//profilePhotoName