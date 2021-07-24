namespace cn.justwin.BLL.AllocUser
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Services;
    using System;
    using System.Collections.Generic;

    public class BasicPriv : IAllocUser
    {
        public void AddUser(IList<string> idList, IList<string> userList, string tableName)
        {
            BasicPrivilegeService service = new BasicPrivilegeService();
            IList<string> subList = idList;
            if (tableName == "XPM_Basic_CodeList")
            {
                subList = new XPMBasicCodeListService().GetSubList(idList[0]);
            }
            foreach (string str in userList)
            {
                foreach (string str2 in subList)
                {
                    BasicPrivilege bp = new BasicPrivilege {
                        PrivilegeId = Guid.NewGuid().ToString(),
                        RelationsTable = tableName,
                        RelationsKey = str2,
                        UserCode = str
                    };
                    if (!service.Exists(bp))
                    {
                        service.Add(bp);
                    }
                }
            }
        }

        public void DeleteExistsUser(string id, string tableName)
        {
            new BasicPrivilegeService().Delete(tableName, id);
        }

        public IList<string> GetExistsUser(string id, string tableName)
        {
            BasicPrivilegeService service = new BasicPrivilegeService();
            return service.GetUser(tableName, id);
        }
    }
}

