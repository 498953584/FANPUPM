namespace cn.justwin.BLL.AllocUser
{
    using System;
    using System.Collections.Generic;

    public interface IAllocUser
    {
        void AddUser(IList<string> idList, IList<string> userList, string tableName);
        void DeleteExistsUser(string id, string tableName);
        IList<string> GetExistsUser(string id, string tableName);
    }
}

