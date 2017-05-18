using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using WITU.Helper.Impls;
using WITU.UserManagement.Identity;

namespace WITU.Utils
{
    public static class PrincipalExtensions
    {
        public static IdentityUser ApplicationIdentityUser(this IPrincipal principal)
        {
            return principal.Identity as IdentityUser;
        }

        public static int ApplicationUserType(this IPrincipal principal)
        {
            //if(!principal.Identity.IsAuthenticated)
            //    throw new ArgumentNullException("principal");

            if (!principal.Identity.IsAuthenticated)
                return -1;

            var userClaims = principal.Identity as ClaimsIdentity;
            var userTypeClaim = userClaims.Claims.FirstOrDefault(x => x.Type.Equals(ApplicationConstants.UserTypeClaim));
            if(userTypeClaim == null)
                throw new IndexOutOfRangeException("No User Claim for userType");

            return Int32.Parse(userTypeClaim.Value);
        }

        public static string ApplicationUserProfilePhoto(this IPrincipal principal)
        {
            string avatarUrl = ApplicationConstants.ProfilesUrl + ApplicationConstants.ImgAvator;
            if (!principal.Identity.IsAuthenticated)
                return null;

            var userClaims = principal.Identity as ClaimsIdentity;
            var userPhotoClaim = userClaims.Claims.FirstOrDefault(x => x.Type.Equals(ApplicationConstants.UserPhotoClaim));
            if (userPhotoClaim == null)
//                throw new IndexOutOfRangeException("No User Claim for userType");
                return null;
            var helper = new GeneralHelper();
            return helper.IsPhotoAvailable(userPhotoClaim.Value) ? userPhotoClaim.Value : avatarUrl;
            //return @userPhotoClaim.Value;
        }

        public static string ApplicationUserAccessRoles(this IPrincipal principal)
        {
            if (!principal.Identity.IsAuthenticated)
                return null;

            var userClaims = principal.Identity as ClaimsIdentity;
            var userTypeClaim = userClaims.Claims.FirstOrDefault(x => x.Type.Equals(ApplicationConstants.UserAccessLevel));
            if (userTypeClaim == null)
                //                throw new IndexOutOfRangeException("No User Claim for userType");
                return null;

            return @userTypeClaim.Value;
        }
        public static int ApplicationStudentProgramId(this IPrincipal principal)
        {
            //if(!principal.Identity.IsAuthenticated)
            //    throw new ArgumentNullException("principal");

            if (!principal.Identity.IsAuthenticated)
                return -1;

            var userClaims = principal.Identity as ClaimsIdentity;
            var studentProgramIdClaim = userClaims.Claims.FirstOrDefault(x => x.Type.Equals(ApplicationConstants.StudentProgramIdClaim));
            if (studentProgramIdClaim == null)
                throw new IndexOutOfRangeException("No User Claim for StudentProgramIdClaim");

            return Int32.Parse(studentProgramIdClaim.Value);
        }

        public static int ApplicationStudentSpecialisationId(this IPrincipal principal)
        {
            //if(!principal.Identity.IsAuthenticated)
            //    throw new ArgumentNullException("principal");

            if (!principal.Identity.IsAuthenticated)
                return -1;

            var userClaims = principal.Identity as ClaimsIdentity;
            var studentSpecialisationIdClaim = userClaims.Claims.FirstOrDefault(x => x.Type.Equals(ApplicationConstants.StudentSpecialisationIdClaim));
            if (studentSpecialisationIdClaim == null)
                throw new IndexOutOfRangeException("No User Claim for StudentSpecialisationIdClaim");

            return Int32.Parse(studentSpecialisationIdClaim.Value);
        }

        //public static string ApplicationUserRoles(this IPrincipal principal)
        //{
        //    if (!principal.Identity.IsAuthenticated)
        //        return null;

        //    var userClaims = principal.Identity as ClaimsIdentity;
        //    var userTypeClaim = userClaims.Claims.FirstOrDefault(x => x.Type.Equals(ApplicationConstants.RolesClaim));
        //    if (userTypeClaim == null)
        //        throw new IndexOutOfRangeException("No User Claim Roles");

        //    return userTypeClaim.Value;
        //}


    }
}