namespace cn.justwin.BLL.AllocUser
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Services;
    using System;
    using System.Collections.Generic;

    public class RoleUser : IAllocUser
    {
        public void AddUser(IList<string> idList, IList<string> userList, string tableName)
        {
            PrivUserRoleService service = new PrivUserRoleService();
            foreach (string str in userList)
            {
                foreach (string str2 in idList)
                {
                    PrivUserRole userRole = new PrivUserRole {
                        Id = Guid.NewGuid().ToString(),
                        RoleId = str2,
                        UserCode = str
                    };
                    if (!service.Exists(userRole))
                    {
                        service.Add(userRole);
                    }
                }
            }
        }

        public void DeleteExistsUser(string id, string tableName)
        {
            new PrivUserRoleService().Delete(id);
        }

        public IList<string> GetExistsUser(string id, string tableName)
        {
            PrivUserRoleService service = new PrivUserRoleService();
            return service.GetUserByRole(id);
        }
    }
}

