using QuanLyBanSachMVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace QuanLyBanSachMVC.Areas.Admin.Models
{
    public class PermissionProvider : RoleProvider
    {
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

        public override string[] GetRolesForUser(string username)
        {
            Debug.WriteLine(username);

            using (QuanLyBanSachEntities db = new QuanLyBanSachEntities())
            {
                var khachhang = db.KhachHangs.FirstOrDefault(n => n.TaiKhoan == username);

                if (khachhang == null)
                    return null;
                else
                {
                    String[] listquyen = khachhang.Quyens.Select(n => n.TenQuyen).ToArray();
                    return listquyen;
                }
            }
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
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