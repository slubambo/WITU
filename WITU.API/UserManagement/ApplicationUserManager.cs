using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WITU.API.UserManagement.Identity;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using Microsoft.AspNet.Identity;

namespace WITU.API.UserManagement
{
    public class ApplicationUserManager : UserManager<IdentityUser, int>
    {
        private readonly IUserStore<IdentityUser, int> _store;

        public ApplicationUserManager(IUserStore<IdentityUser, int> store) : base(store)
        {
            _store = store;
        }
    }
}