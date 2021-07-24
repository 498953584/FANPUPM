namespace cn.justwin.TechnologyManage
{
    using cn.justwin.DAL;
    using PmBusinessLogic;
    using System;
    using System.Data;

    public class TechnologyManage : ISelfEvent
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
            string cmdText = string.Format("SELECT * FROM Prj_TechnologyManage WHERE techGuid='{0}'", key.ToString());
            DataTable table = SqlHelper.ExecuteQuery(CommandType.Text, cmdText, null);
            if (table.Rows.Count > 0)
            {
                string str2 = table.Rows[0]["id"].ToString();
                switch (table.Rows[0]["bigSort"].ToString())
                {
                    case "2":
                    case "3":
                    case "4":
                    case "8":
                    {
                        string str4 = "";
                        str4 = "begin";
                        SqlHelper.ExecuteNonQuery(CommandType.Text, (((str4 + " delete from XPM_Basic_AnnexList where ModuleID=1726 and RecordCode ='" + str2 + "'") + " delete from Prj_TechnologyManage where ID = " + str2) + " end").ToString(), null);
                        break;
                    }
                }
            }
        }
    }
}

