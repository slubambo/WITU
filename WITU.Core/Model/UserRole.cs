using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WITU.Core.Model
{
    [Serializable]
    public class UserRole
    {
        public virtual int Id { get; set; }

        public virtual User User { get; set; }

        public virtual AccessRole AccessRole { get; set; }

        public virtual Role Role { get; set; }
    }

    public class UserDetails
    {
        public virtual List<UserRole> UserRoles { get; set; } 
    }

}
