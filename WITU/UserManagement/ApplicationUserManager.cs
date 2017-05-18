using WITU.UserManagement.Identity;
using Microsoft.AspNet.Identity;

namespace WITU.UserManagement
{
    public class ApplicationUserManager : UserManager<IdentityUser, int>
    {
        private readonly IUserStore<IdentityUser, int> _store;

        public ApplicationUserManager(IUserStore<IdentityUser, int> store) : base(store)
        {
            _store = store;
            this.PasswordHasher = new SqlPasswordHasher();
        }
    }
}