using Microsoft.AspNet.Identity;

namespace WITU.API.UserManagement.Identity
{
    public enum IdentityStatus
    {
        Normal,

        Transfered,

        FirstTimeUser
    }
    public class IdentityUser: IUser<int>
    {
        public IdentityUser()
        {
            
        }

        public IdentityUser(string userName) : this()
        {
            UserName = userName;
        }

        public int Id { get; set; }

        public string UserName { get; set; }

        public object PasswordHash { get; set; }

        public int UserType { get; set; }

        public bool IsFirstTimeUser { get; set; }

        public bool IsApproved { get; set; }

        public string Email { get; set; }

        public string SecurityStamp { get; set; }

        public string ProfilePhotoName { get; set; }

        

    }
}