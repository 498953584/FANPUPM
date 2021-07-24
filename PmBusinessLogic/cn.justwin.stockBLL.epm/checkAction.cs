namespace cn.justwin.stockBLL.epm
{
    using cn.justwin.DAL;
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class checkAction
    {
        public bool del(string id, SqlTransaction trans)
        {
            int num = 0;
            string str = "DELETE FROM Prj_ItemInspect WHERE ID=" + id;
            if (trans == null)
            {
                num = SqlHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), null);
            }
            else
            {
                num = SqlHelper.ExecuteNonQuery(trans, CommandType.Text, str.ToString(), null);
            }
            return (num > 0);
        }
    }
}

