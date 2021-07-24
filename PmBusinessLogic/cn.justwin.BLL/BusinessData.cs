namespace cn.justwin.BLL
{
    using cn.justwin.DAL;
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Services;
    using System;
    using System.Data;

    public class BusinessData
    {
        public static void Delete(string busiCode, string guid)
        {
            WFBusinessCode byId = new WFBusinessCodeService().GetById(busiCode);
            string cmdText = string.Format("SELECT TOP(1) {0} FROM {1} WHERE {2} = '{3}'", new object[] { byId.StateField, byId.LinkTable, byId.PrimaryField, guid });
            string str2 = string.Format("DELETE FROM {0} WHERE {1} = '{2}'", byId.LinkTable, byId.PrimaryField, guid);
            string str3 = string.Format("DELETE FROM WF_Instance_Main WHERE InstanceCode = '{0}'", guid);
            switch (DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, cmdText)))
            {
                case -1:
                    SqlHelper.ExecuteNonQuery(CommandType.Text, str2);
                    return;

                case 1:
                    SelfEventAction.SuperDelete(guid, byId.LinkTable, byId.StateField);
                    SqlHelper.ExecuteNonQuery(CommandType.Text, str3);
                    SqlHelper.ExecuteNonQuery(CommandType.Text, str2);
                    return;
            }
            SqlHelper.ExecuteNonQuery(CommandType.Text, str3);
            SqlHelper.ExecuteNonQuery(CommandType.Text, str2);
        }
    }
}

