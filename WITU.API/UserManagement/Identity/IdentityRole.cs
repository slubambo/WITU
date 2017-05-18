using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace WITU.API.UserManagement.Identity
{
    public class IdentityRole : IRole<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ProfilePhotoName { get; set; }
    }
}