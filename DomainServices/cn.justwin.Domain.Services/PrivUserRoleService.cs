namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PrivUserRoleService : Repository<PrivUserRole>
    {
        public void Delete(string roleId)
        {
            string sql = string.Format("DELETE FROM Priv_UserRole WHERE RoleId = '{0}'", roleId);
            base.ExcuteSql(sql);
        }

        public bool Exists(PrivUserRole userRole)
        {
            return ((from ur in this
                where (ur.RoleId == userRole.RoleId) && (ur.UserCode == userRole.UserCode)
                select ur).Count<PrivUserRole>() > 0);
        }

        public PrivUserRole GetById(string id)
        {
            return (from ur in this
                where ur.Id == id
                select ur).FirstOrDefault<PrivUserRole>();
        }

        public IList<string> GetRoleByUserCode(string userCode)
        {
            return (from ur in this
                where ur.UserCode == userCode
                select ur.RoleId).ToList<string>();
        }

        public IList<string> GetUserByRole(string roleId)
        {
            return (from ur in this
                where ur.RoleId == roleId
                select ur.UserCode).ToList<string>();
        }
    }
}

