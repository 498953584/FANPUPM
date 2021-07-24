namespace cn.justwin.BLL.AllocUser
{
    using cn.justwin.BLL.ProgressManagement;
    using System;
    using System.Collections.Generic;

    public class ProgressPlanUser : IAllocUser
    {
        public void AddUser(IList<string> idList, IList<string> userList, string tableName)
        {
            foreach (string str in idList)
            {
                Privilege.AddPrivilegeNoDelete(str, userList);
            }
        }

        public void DeleteExistsUser(string id, string tableName)
        {
            Privilege.Delete(id);
        }

        public IList<string> GetExistsUser(string id, string tableName)
        {
            return Privilege.GetUserCodes(id);
        }
    }
}

