namespace cn.justwin.DAL
{
    using System;
    using System.Data;

    public class ConstructOrganizeService
    {
        public DataTable getDataTable(string DataTableName, string strwhere, string orderby)
        {
            string str = "select * from " + DataTableName;
            if (strwhere != "")
            {
                str = str + " where " + strwhere;
            }
            if (orderby != "")
            {
                str = str + " order by " + orderby;
            }
            return SqlHelper.ExecuteQuery(CommandType.Text, str.ToString(), null);
        }

        public DataTable getModelByFlow(string DataTableName, Guid FlowGuid)
        {
            string str = string.Concat(new object[] { "select * from ", DataTableName, " where FlowGuid='", FlowGuid, "'" });
            return SqlHelper.ExecuteQuery(CommandType.Text, str.ToString(), null);
        }

        public int UpdGuidang(string tablename, int mark, int filesType, string where)
        {
            string str = "update " + tablename + " set ";
            if (!string.IsNullOrEmpty(mark.ToString()))
            {
                str = str + " mark = " + mark;
            }
            if (!string.IsNullOrEmpty(filesType.ToString()) && !string.IsNullOrEmpty(mark.ToString()))
            {
                str = str + ", filesType = " + filesType;
            }
            if (!string.IsNullOrEmpty(where))
            {
                str = str + where;
            }
            return SqlHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), null);
        }
    }
}

