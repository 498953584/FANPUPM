namespace cn.justwin.TechnologyManage
{
    using cn.justwin.DAL;
    using PmBusinessLogic;
    using System;
    using System.Data;

    public class TechnologyTell : ISelfEvent
    {
        public void CommitEvent(object key)
        {
        }

        public void RefuseEvent(object key)
        {
        }

        public void RestatedEvent(object key)
        {
        }

        public void SuperDelete(object key)
        {
            string cmdText = string.Format("SELECT * FROM Prj_TechnologyTell WHERE techGuid='{0}'", key.ToString());
            DataTable table = SqlHelper.ExecuteQuery(CommandType.Text, cmdText, null);
            if (table.Rows.Count > 0)
            {
                string str2 = table.Rows[0]["MainId"].ToString();
                string str3 = string.Empty;
                str3 = "begin";
                SqlHelper.ExecuteNonQuery(CommandType.Text, (((str3 + " delete from XPM_Basic_AnnexList where ModuleID=1728 and RecordCode ='" + str2 + "'") + " delete from Prj_TechnologyTell where MainId = " + str2) + " end").ToString(), null);
            }
        }
    }
}

