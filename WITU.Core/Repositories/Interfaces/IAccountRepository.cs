using System.Collections.Generic;
using WITU.Core.Model;
using FluentNHibernate.Data;

namespace WITU.Core.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        string GetUserName(int userId);

        int GetUserId(string username);

        User GetUserByName(string username);

        List<UserRole> UserRoles(int userId);
        
        string GetPasswordHash(int userId);

        string GetRoleName(int roleId);

        Role GetRoleByName(string roleName);

        bool IsUserInRole(int userId, string roleName);

        
        Instructor GetUserInstructor(User user);

        Instructor GetUserInstructor(int userId);

        Student GetUserStudent(User user);

       ProspectiveStudent GetUserProseProspectiveStudent(User user);

    }
}
