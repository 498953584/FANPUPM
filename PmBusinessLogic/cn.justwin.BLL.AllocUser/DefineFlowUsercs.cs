namespace cn.justwin.BLL.AllocUser
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Services;
    using System;
    using System.Collections.Generic;

    public class DefineFlowUsercs : IAllocUser
    {
        public void AddUser(IList<string> idList, IList<string> userList, string tableName)
        {
            WFprivilegeService service = new WFprivilegeService();
            foreach (string str in userList)
            {
                foreach (string str2 in idList)
                {
                    WFprivilege priv = new WFprivilege {
                        businessClass = str2,
                        userlist = str
                    };
                    if (!service.Exists(priv))
                    {
                        service.Add(priv);
                    }
                }
            }
        }

        public void DeleteExistsUser(string id, string tableName)
        {
            new WFprivilegeService().Delete(id);
        }

        public IList<string> GetExistsUser(string id, string tableName)
        {
            WFprivilegeService service = new WFprivilegeService();
            return service.GetUser(id);
        }
    }
}

