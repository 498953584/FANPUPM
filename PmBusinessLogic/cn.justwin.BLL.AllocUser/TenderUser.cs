namespace cn.justwin.BLL.AllocUser
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Services;
    using System;
    using System.Collections.Generic;

    public class TenderUser : IAllocUser
    {
        public void AddUser(IList<string> idList, IList<string> userList, string tableName)
        {
            PTPrjInfoZTBUserService service = new PTPrjInfoZTBUserService();
            foreach (string str in userList)
            {
                foreach (string str2 in idList)
                {
                    PTPrjInfoZTBUser tu = new PTPrjInfoZTBUser {
                        Id = Guid.NewGuid().ToString(),
                        PrjGuid = str2,
                        UserCode = str
                    };
                    if (!service.Exists(tu))
                    {
                        service.Add(tu);
                    }
                }
            }
        }

        public void DeleteExistsUser(string id, string tableName)
        {
            new PTPrjInfoZTBUserService().Delete(id);
        }

        public IList<string> GetExistsUser(string id, string tableName)
        {
            PTPrjInfoZTBUserService service = new PTPrjInfoZTBUserService();
            return service.GetUser(id);
        }
    }
}

