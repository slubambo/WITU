using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WITU.Core.Model;
using WITU.Core.Repositories.Interfaces;
using WITU.UserManagement.Identity;
using Microsoft.AspNet.Identity;
using Ninject;
using Remotion.Linq.Utilities;

namespace WITU.UserManagement.Store
{
    public class UserStore : IUserStore<IdentityUser, int>, IUserRoleStore<IdentityUser, int>,
        IUserPasswordStore<IdentityUser, int>, IUserLoginStore<IdentityUser, int>, IRoleStore<IdentityRole, int>
    {
        private readonly IAccountRepository _accountRepository;

        private readonly ICoreRepository _coreRepository;

        public UserStore(IAccountRepository accountRepository, ICoreRepository coreRepository)
        {
            _coreRepository = coreRepository;
            _accountRepository = accountRepository;
        }

        #region UserStore Impl
        public Task CreateAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var coreUser = new User()
            {
                Username = user.UserName,
                Email = user.Email,
                Password = user.PasswordHash.ToString(),
                UserType = user.UserType,
                PasswordKey = user.SecurityStamp,
                IsFirstTimeUser = user.IsFirstTimeUser,
                IsApproved = user.IsApproved,
                IsLockedOut = user.IsLockedOut,
                ProfilePhotoName = user.ProfilePhotoName ?? "avatar.jpg",
              };

            _coreRepository.SaveOrUpdate(coreUser);

            return Task.FromResult<object>(null);
        }

        public Task DeleteAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityUser> FindByIdAsync(int userId)
        {
            if (userId == 0)
                throw new ArgumentException("User Id cannot be 0");

            //otherwise we get the user and put him in identity...
            var user = _coreRepository.Get<User>(userId);

            var profilePhoto = "";
            switch (user.UserType)
            {
                case (int)UserTypes.Instructor:
                    var instructor = _accountRepository.GetUserInstructor(user);
                    profilePhoto = instructor.Person?.ProfilePhotoName;
                    break;

                case (int)UserTypes.Student:
                    var student = _accountRepository.GetUserStudent(user);
                    profilePhoto = student.Person != null ? student.Person.ProfilePhotoName : null;
                    break;

                case (int)UserTypes.ProspectiveStudent:
                    var prospectiveStudent = _accountRepository.GetUserProseProspectiveStudent(user);
                    profilePhoto = prospectiveStudent.Person != null ? prospectiveStudent.Person.ProfilePhotoName : null;
                    break;
            }
            
            if (user == null)
                return Task.FromResult<IdentityUser>(null);

            var identityUser = new IdentityUser()
            {
                Id = user.Id,
                UserName = user.Username,
                PasswordHash = user.Password,
                UserType = user.UserType,
                IsFirstTimeUser = user.IsFirstTimeUser,
                IsApproved = user.IsApproved,
                IsLockedOut = user.IsLockedOut,
                SecurityStamp = user.PasswordKey,
                Email = user.Email,
                ProfilePhotoName = profilePhoto ?? "avatar.jpg"
                //AltProfilePhotoName = GetProfilePhotoName(user)
            };

            return Task.FromResult(identityUser);
        }

        
        public Task<IdentityUser> FindByNameAsync(string userName)
        {
            if (String.IsNullOrEmpty(userName))
                throw new ArgumentException("UserName cannot be null or empty!!");

            var user = _accountRepository.GetUserByName(userName);
            if (user == null)
                return Task.FromResult<IdentityUser>(null);

            var profilePhoto = "";
            switch (user.UserType)
            {
                case (int)UserTypes.Instructor:
                    var instructor = _accountRepository.GetUserInstructor(user);
                    if (instructor != null)
                    {
                        profilePhoto = instructor.Person?.ProfilePhotoName;
                    }
                    break;

                case (int)UserTypes.Student:
                    var student = _accountRepository.GetUserStudent(user);
                    if (student != null)
                    {
                        profilePhoto = student.Person?.ProfilePhotoName;
                    }
                    break;

                case (int)UserTypes.ProspectiveStudent:
                    var prospectiveStudent = _accountRepository.GetUserProseProspectiveStudent(user);
                    profilePhoto = prospectiveStudent.Person?.ProfilePhotoName;
                    break;
            }

            var identityUser = new IdentityUser()
            {
                Id = user.Id,
                UserName = user.Username,
                PasswordHash = user.Password,
                UserType = user.UserType,
                IsApproved = user.IsApproved,
                IsLockedOut = user.IsLockedOut,
                IsFirstTimeUser = user.IsFirstTimeUser,
                Email = user.Email,
                SecurityStamp = user.PasswordKey,
                ProfilePhotoName = profilePhoto ?? "avatar.jpg"
            };

            return Task.FromResult(identityUser);
        }

        public Task UpdateAsync(IdentityUser user)
        {
            //get user to update..
            var actualUser = _accountRepository.GetUserByName(user.UserName);
            actualUser.Password = user.PasswordHash != null ?user.PasswordHash.ToString():null;
            actualUser.IsLockedOut = user.IsLockedOut;
            actualUser.IsApproved = user.IsApproved;
            actualUser.Email = user.Email;
            actualUser.PasswordKey = user.SecurityStamp;
            actualUser.UserType = user.UserType;
            actualUser.ProfilePhotoName = user.ProfilePhotoName;

            //then update the user...
            _coreRepository.SaveOrUpdate(actualUser);

            return Task.FromResult<object>(null);
        }

        public void Dispose()
        {
            //            throw new NotImplementedException();
        }
        #endregion

        #region UserRole Store Impl
        public Task AddToRoleAsync(IdentityUser user, string roleName)
        {
            var role = _accountRepository.GetRoleByName(roleName);
            if (role == null)
                throw new ArgumentException(string.Format("No Role with name: {0}", roleName));

            throw new NotImplementedException();
        }

        public Task<IList<string>> GetRolesAsync(IdentityUser user)
        {
            var u = _coreRepository.Get<User>(user.Id);
            if (u == null)
                return Task.FromResult<IList<string>>(new List<string>());

            //if user is de we get the roles...
            var roles = u.UserRoles.Select(x => x.Role.Service.Name).ToList();
            return Task.FromResult<IList<string>>(roles);
        }

        public Task<bool> IsInRoleAsync(IdentityUser user, string roleName)
        {
            var isUserInRole = _accountRepository.IsUserInRole(user.Id, roleName);
            return Task.FromResult(isUserInRole);
        }

        public System.Threading.Tasks.Task RemoveFromRoleAsync(IdentityUser user, string roleName)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region UserPasswordStore Impl

        public Task<string> GetPasswordHashAsync(IdentityUser user)
        {
            var password = _accountRepository.GetPasswordHash(user.Id);
            return Task.FromResult<string>(password);
        }

        public Task<bool> HasPasswordAsync(IdentityUser user)
        {
            var hasPassword = !string.IsNullOrEmpty(_accountRepository.GetPasswordHash(user.Id));
            return Task.FromResult<bool>(hasPassword);
        }

        public Task SetPasswordHashAsync(IdentityUser user, string passwordHash)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult<object>(null);
        }
        #endregion

        #region User Login Impl
        public Task AddLoginAsync(IdentityUser user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public Task RemoveLoginAsync(IdentityUser user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(IdentityUser user)
        {
            var userLoginInfos = new List<UserLoginInfo>();
            return Task.FromResult<IList<UserLoginInfo>>(userLoginInfos);
        }

        public Task<IdentityUser> FindAsync(UserLoginInfo login)
        {
            return Task.FromResult<IdentityUser>(null);
        }

        #endregion

        #region RoleStore Impl
        public Task CreateAsync(IdentityRole role)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(IdentityRole role)
        {
            throw new NotImplementedException();
        }

        Task<IdentityRole> IRoleStore<IdentityRole, int>.FindByIdAsync(int roleId)
        {
            throw new NotImplementedException();
        }

        Task<IdentityRole> IRoleStore<IdentityRole, int>.FindByNameAsync(string roleName)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(IdentityRole role)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}