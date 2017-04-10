using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.ComponentModel;
using Web.Datanomics.Models;

namespace Web.Datanomics
{
    public class CustomRoleProvider : System.Web.Security.RoleProvider
    {
        //su ly code o hàm này
        public override string[] GetRolesForUser(string username)
        {
            //code logic toi csdl để lấy quyền
            DataContext db = new DataContext();

            return (from p in db.Roles
                    where p.ID == (from u in db.UserProfiles
                                   where u.UserName == username
                                   select u.RoleID).FirstOrDefault()
                    select p.Name).ToArray();
        }
        //ham nay khong can thiet
        public override bool IsUserInRole(string username, string roleName)
        {
            if (username.Equals("") && roleName.Equals(""))
            {
                return true;
            }
            else if (username.Equals("") && roleName.Equals(""))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }



        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}
