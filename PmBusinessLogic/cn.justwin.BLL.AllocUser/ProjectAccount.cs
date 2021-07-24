namespace cn.justwin.BLL.AllocUser
{
    using cn.justwin.BLL;
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    public class ProjectAccount : IAllocUser
    {
        public void AddUser(IList<string> idList, IList<string> userList, string tableName)
        {
            string str = JsonHelper.JsonSerializer<string[]>(userList.ToArray<string>());
            foreach (string str2 in idList)
            {
                string cmdText = string.Format("UPDATE Fund_Prj_Accoun SET authorizer = '{0}' WHERE AccountID = '{1}'", str, str2);
                SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText);
            }
        }

        public void DeleteExistsUser(string id, string tableName)
        {
            string cmdText = string.Format("UPDATE Fund_Prj_Accoun SET authorizer = '[\"00000000\"]' WHERE AccountID = '{0}'", id);
            SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText);
        }

        public IList<string> GetExistsUser(string id, string tableName)
        {
            string cmdText = string.Format("SELECT authorizer FROM Fund_Prj_Accoun WHERE AccountID = '{0}'", id);
            return JsonHelper.GetListFromJson(DBHelper.GetString(SqlHelper.ExecuteScalar(CommandType.Text, cmdText)));
        }
    }
}

