using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using WITU.API.UserManagement.Identity;

namespace WITU.API.Utils
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
            if (!principal.Identity.IsAuthenticated)
                return null;

            var userClaims = principal.Identity as ClaimsIdentity;
            var userTypeClaim = userClaims.Claims.FirstOrDefault(x => x.Type.Equals(ApplicationConstants.UserPhotoClaim));
            if (userTypeClaim == null)
//                throw new IndexOutOfRangeException("No User Claim for userType");
                return null;

            return @userTypeClaim.Value;
        }


    }
}