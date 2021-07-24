namespace cn.justwin.BLL.AllocUser
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class MkUser : IAllocUser
    {
        public void AddUser(IList<string> idList, IList<string> userList, string tableName)
        {
            PTYHMCPrivilegeService service = new PTYHMCPrivilegeService();
            PTMKService service2 = new PTMKService();
            List<string> first = new List<string>();
            first = first.Union<string>(idList).Distinct<string>().ToList<string>();
            foreach (string str in idList)
            {
                IList<string> childrenAndSelf = service2.GetChildrenAndSelf(str);
                first = first.Union<string>(childrenAndSelf).Distinct<string>().ToList<string>();
            }
            IList<string> parentAll = service2.GetParentAll(idList[0]);
            first = first.Union<string>(parentAll).ToList<string>();
            foreach (string str2 in userList)
            {
                foreach (string str3 in first)
                {
                    PTYHMCPrivilege yp = new PTYHMCPrivilege {
                        Id = Guid.NewGuid().ToString(),
                        V_YHDM = str2,
                        C_MKDM = str3
                    };
                    if (!service.Exists(yp))
                    {
                        service.Add(yp);
                    }
                }
            }
        }

        public void DeleteExistsUser(string id, string tableName)
        {
            PTYHMCPrivilegeService service = new PTYHMCPrivilegeService();
            PTMKService service2 = new PTMKService();
            foreach (string str in service2.GetChildrenAndSelf(id))
            {
                service.DeleteByMk(str);
            }
        }

        public IList<string> GetExistsUser(string id, string tableName)
        {
            PTYHMCPrivilegeService service = new PTYHMCPrivilegeService();
            return service.GetUser(id);
        }
    }
}

